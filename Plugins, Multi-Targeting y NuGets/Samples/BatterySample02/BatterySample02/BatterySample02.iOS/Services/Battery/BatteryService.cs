using BatterySample02.iOS.Services;
using BatterySample02.Services;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(BatteryService))]
namespace BatterySample02.iOS.Services
{
    public class BatteryService : IBatteryService
    {
        public int GetBatteryStatus()
        {
            return (int)(UIDevice.CurrentDevice.BatteryLevel * 100F);
        }
    }
}