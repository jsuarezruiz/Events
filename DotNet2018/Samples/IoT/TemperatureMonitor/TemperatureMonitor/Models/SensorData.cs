using System;

namespace TemperatureMonitor.Models
{
    public class SensorData
    {
        public int Temperature { get; set; }
        public int Humidity { get; set; }
        public string Date { get; set; }
    }
}