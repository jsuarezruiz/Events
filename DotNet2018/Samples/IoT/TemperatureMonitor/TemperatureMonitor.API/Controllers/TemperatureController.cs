using Microsoft.AspNetCore.Mvc;
using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.WindowsAzure.Storage;
using System;
using System.Threading.Tasks;
using System.Linq;
using AutoMapper;
using TemperatureMonitor.API.Models;
using NodaTime;
using System.Collections.Generic;
using TemperatureMonitor.API.Extensions;
using TemperatureMonitor.Shared;

namespace TemperatureMonitor.API.Controllers
{
    [Route("api/[controller]")]
    public class TemperatureController : Controller
    {
        private readonly CloudTableClient _client;
        private readonly CloudTable _table;
        private readonly string _tableName;

        public TemperatureController()
        {
            var storageAccount = CloudStorageAccount.Parse(AppSettings.StorageConnectionString);
            _client = storageAccount.CreateCloudTableClient();
            _tableName = AppSettings.StorageTableName;
            _table = _client.GetTableReference(_tableName);
        }

        [HttpGet]
        public async Task<IActionResult> Get(string timeZone)
        {
            var operation = new TableQuery<SensorDataTableEntity>()
                .Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, AppSettings.StoragePartitionKey))
                .Take(1);

            var temperature = (await _table.ExecuteQuerySegmentedAsync(operation, null)).FirstOrDefault();

            if (temperature != null)
            {
                if (string.IsNullOrEmpty(timeZone))
                {
                    timeZone = AppSettings.DefaultTimeZone;
                }

                var dateTimeZone = DateTimeZoneProviders.Tzdb.GetZoneOrNull(timeZone);

                if (dateTimeZone == null)
                {
                    return BadRequest($"Unknown time zone: {timeZone}");
                }

                return Ok(Mapper.Map<SensorData>(temperature, opts =>
                {
                    opts.Items["DateTimeZone"] = dateTimeZone;
                }));
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Route("summary")]
        public async Task<IActionResult> Summary(string timeZone, DateTime date)
        {
            if (date == new DateTime())
            {
                date = DateTime.Today;
            }

            if (string.IsNullOrEmpty(timeZone))
            {
                timeZone = AppSettings.DefaultTimeZone;
            }

            var dateTimeZone = DateTimeZoneProviders.Tzdb.GetZoneOrNull(timeZone);

            if (dateTimeZone == null)
            {
                return BadRequest($"Unknown time zone: {timeZone}");
            }

            var zonedDateTime = LocalDateTime.FromDateTime(date).InZoneLeniently(dateTimeZone);
            var result = await GetTemperatureDataForDate(zonedDateTime);

            return Ok(Mapper.Map<List<SensorData>>(result, opts =>
            {
                opts.Items["DateTimeZone"] = dateTimeZone;
            }));
        }

        private async Task<List<SensorDataTableEntity>> GetTemperatureDataForDate(ZonedDateTime zonedDateTime)
        {
            var dateTimeZone = zonedDateTime.Zone;
            var startDateTime = zonedDateTime.ToDateTimeUtc();
            var endDateTime = zonedDateTime.PlusHours(24).ToDateTimeUtc();

            var requestStartTick = startDateTime.ToTicks();
            var requestEndTick = endDateTime.ToTicks();

            var operation = new TableQuery<SensorDataTableEntity>()
               .Where(TableQuery.CombineFilters(
                   TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, AppSettings.StoragePartitionKey),
                   TableOperators.And,
                   TableQuery.CombineFilters(
                       TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.GreaterThanOrEqual, requestStartTick),
                       TableOperators.And,
                       TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.LessThan, requestEndTick))));

            return (await _table.ExecuteQuerySegmentedAsync(operation, null)).ToList();
        }
    }
}