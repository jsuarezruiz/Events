using Plugin.BatteryPlugin.Abstractions;
using UIKit;

namespace Plugin.BatteryPlugin
{
    public class BatteryImplementation : BaseBatteryImplementation
    {
        public override int GetBatteryStatus()
        {
            return (int)(UIDevice.CurrentDevice.BatteryLevel * 100F);
        }
    }
}