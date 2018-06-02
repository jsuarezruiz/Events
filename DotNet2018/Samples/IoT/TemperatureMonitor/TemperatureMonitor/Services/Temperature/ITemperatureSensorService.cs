using TemperatureMonitor.Models;

namespace TemperatureMonitor.Services.Temperature
{
    public interface ITemperatureSensorService
    {
        SensorData GetData();
    }
}