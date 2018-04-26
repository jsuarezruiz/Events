using Android.Content;
using Android.OS;
using BatterySample02.Droid.Services;
using BatterySample02.Services;
using System;
using Xamarin.Forms;

[assembly: Dependency(typeof(BatteryService))]
namespace BatterySample02.Droid.Services
{
    public class BatteryService : IBatteryService
    {
        public int GetBatteryStatus()
        {
            try
            {
                using (var filter = new IntentFilter(Intent.ActionBatteryChanged))
                {
                    using (var battery = Android.App.Application.Context.RegisterReceiver(null, filter))
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