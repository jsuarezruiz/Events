namespace TemperatureMonitor.Shared
{
    public static class AppSettings
    {
        public static string StorageConnectionString = "DefaultEndpointsProtocol=https;AccountName=temperaturemonitor;AccountKey=1E2nvlChXwX5LygucCgVGHwDo/iJJ2QjzBd5GlcELX09H64lccZo6mxOE6onUjJGxcz2IZpfqZN38204ly+w7Q==;EndpointSuffix=core.windows.net";
        public static string StorageTableName = "TemperatureMonitor";
        public static string StoragePartitionKey = "TemperatureKey";
        public static string ApiUrl = "http://temperaturemonitorapi.azurewebsites.net";
        public static string DefaultTimeZone = "Europe/Madrid";
    }
}