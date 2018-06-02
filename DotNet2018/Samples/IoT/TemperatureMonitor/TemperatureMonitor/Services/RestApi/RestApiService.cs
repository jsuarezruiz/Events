using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using TemperatureMonitor.Models;
using TemperatureMonitor.Shared;

namespace TemperatureMonitor.Services.RestApi
{
    public class RestApiService : IRestApiService
    {
        private HttpClient _client;

        private HttpClient Client
        {
            get
            {
                if (_client == null)
                {
                    _client = new HttpClient { BaseAddress = new Uri(AppSettings.ApiUrl) };
                }

                return _client;
            }
        }

        public async Task<SensorData> GetLatestAsync(string timezone)
        {
            var result = await Client.GetAsync($"/api/temperature?&timeZone={timezone}");

            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var content = await result.Content.ReadAsStringAsync();
                var sensorData = JsonConvert.DeserializeObject<SensorData>(content);

                if (sensorData != null)
                {
                    return sensorData;
                }
            }

            return null;
        }

        public async Task<List<SensorData>> GetDateSummaryAsync(string timezone, DateTime date)
        {
            var result = await Client.GetAsync($"/api/temperature/summary?timeZone={timezone}&date={date:yyyy-MM-dd}");

            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var content = await result.Content.ReadAsStringAsync();
                var summary = JsonConvert.DeserializeObject<List<SensorData>>(content);

                if (summary != null)
                {
                    return summary;
                }
            }

            return null;
        }
    }
}