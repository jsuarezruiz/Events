using Android.Content;
using Android.OS;
using Android.App;
using System;
using Plugin.BatteryPlugin.Abstractions;

namespace Plugin.BatteryPlugin
{
    public class BatteryImplementation : BaseBatteryImplementation
    {
        public override int GetBatteryStatus()
        {
            try
            {
                using (var filter = new IntentFilter(Intent.ActionBatteryChanged))
                {
                    using (var battery = Application.Context.RegisterReceiver(null, filter))
                    {
                        var level = battery.GetIntExtra(BatteryManager.ExtraLevel, -1);
                        var scale = battery.GetIntExtra(BatteryManager.ExtraScale, -1);

                        return (int)Math.Floor(level * 100D / scale);
                    }
                }
            }
            catch
            {
                return 0;
            }
        }
    }
}