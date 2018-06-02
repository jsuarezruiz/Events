using AutoMapper;
using NodaTime;
using System;
using TemperatureMonitor.API.Models;

namespace TemperatureMonitor.API
{
    public class ApiAutoMapperProfile : Profile
    {
        public ApiAutoMapperProfile()
        {
            CreateMap<SensorDataTableEntity, SensorData>()
                .ForMember(vm => vm.Location, opt => opt.MapFrom(m => m.PartitionKey))
                .ForMember(vm => vm.Temperature, opt => opt.MapFrom(m => m.Temperature))
                .ForMember(vm => vm.Humidity, opt => opt.MapFrom(m => m.Humidity))
                .ForMember(vm => vm.Date, opt => opt.ResolveUsing(ConvertTicksToDateTime));
        }

        private object ConvertTicksToDateTime(
            SensorDataTableEntity source, 
            SensorData destination, 
            string destMember, 
            ResolutionContext context)
        {
            if (string.IsNullOrWhiteSpace(source.RowKey))
            {
                return null;
            }

            context.Items.TryGetValue("DateTimeZone", out var ianaTimeZone);

            var dateTimeZone = ianaTimeZone as DateTimeZone;

            if (dateTimeZone == null)
            {
                return null;
            }

            var rowTicks = source.RowKey;
            DateTime dateTime = new DateTime();

            if (long.TryParse(rowTicks, out var ticks))
            {
                dateTime = new DateTime(ticks, DateTimeKind.Utc);
            }

            var zonedDateTime = 
                ZonedDateTime.FromDateTimeOffset(new DateTimeOffset(dateTime, TimeSpan.Zero));

            return zonedDateTime.ToOffsetDateTime().ToString();
        }
    }
}