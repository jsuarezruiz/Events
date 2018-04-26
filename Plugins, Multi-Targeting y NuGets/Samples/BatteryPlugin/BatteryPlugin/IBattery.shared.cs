using System;

namespace Plugin.BatteryPlugin.Abstractions
{
    public interface IBattery : IDisposable
    {
        int GetBatteryStatus();
    }
}
