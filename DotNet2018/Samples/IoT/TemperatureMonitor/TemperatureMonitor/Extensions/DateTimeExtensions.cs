using System;

namespace TemperatureMonitor.Extensions
{
    public static class DateTimeExtensions
    {
        public static string ToTicks(this DateTime dateTime)
        {
            return (dateTime.Ticks).ToString("d19");
        }
    }
}