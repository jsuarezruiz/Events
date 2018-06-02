namespace TemperatureMonitor.Shared
{
    public static class AppSettings
    {
        public static string StorageConnectionString = "INSERT YOUR STORAGE CONNECTION HERE";
        public static string StorageTableName = "TemperatureMonitor";
        public static string StoragePartitionKey = "TemperatureKey";
        public static string ApiUrl = "http://temperaturemonitorapi.azurewebsites.net";
        public static string DefaultTimeZone = "Europe/Madrid";
    }
}