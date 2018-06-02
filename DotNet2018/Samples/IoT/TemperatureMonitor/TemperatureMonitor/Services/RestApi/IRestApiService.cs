using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TemperatureMonitor.Models;

namespace TemperatureMonitor.Services.RestApi
{
    public interface IRestApiService
    {
        Task<SensorData> GetLatestAsync(string timezone);

        Task<List<SensorData>> GetDateSummaryAsync(string timezone, DateTime date);
    }
}