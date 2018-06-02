namespace TemperatureMonitor.API.Models
{
    public class SensorData
    {
        public string Location { get; set; }
        public string Date { get; set; }
        public int? Temperature { get; set; }
        public int? Humidity { get; set; }
    }
}