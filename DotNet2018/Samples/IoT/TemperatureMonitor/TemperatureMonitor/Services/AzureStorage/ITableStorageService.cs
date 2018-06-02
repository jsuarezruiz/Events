using System.Threading.Tasks;
using TemperatureMonitor.Models;

namespace TemperatureMonitor.Services.AzureStorage
{
    public interface ITableStorageService
    {
        Task<bool> SaveDataAsync(SensorData data);
    }
}