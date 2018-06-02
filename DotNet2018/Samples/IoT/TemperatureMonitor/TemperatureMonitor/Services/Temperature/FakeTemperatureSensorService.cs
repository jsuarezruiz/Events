using System;
using TemperatureMonitor.Models;

namespace TemperatureMonitor.Services.Temperature
{
    public class FakeTemperatureSensorService : ITemperatureSensorService
    {
        private readonly Random _rand;

        public FakeTemperatureSensorService()
        {
            _rand = new Random(DateTime.Now.Millisecond);
        }

        public SensorData GetData()
        {
            return new SensorData
            {
                Temperature = _rand.Next(0, 45),
                Humidity = _rand.Next(30, 130),
                Date = DateTime.Now.ToString()
            };
        }
    }
}