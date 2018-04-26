using BatterySample02.Services;
using BatterySample02.UWP.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(BatteryService))]
namespace BatterySample02.UWP.Services
{
    public class BatteryService : IBatteryService
    {
        public int GetBatteryStatus()
        {
            var defaultBattery = Windows.Devices.Power.Battery.AggregateBattery;
            var finalReport = defaultBattery.GetReport();
            var percent = -1;

            if (finalReport.RemainingCapacityInMilliwattHours.HasValue && finalReport.FullChargeCapacityInMilliwattHours.HasValue)
            {
                percent = (int)((finalReport.RemainingCapacityInMilliwattHours.Value /
                                 (double)finalReport.FullChargeCapacityInMilliwattHours.Value) * 100);
            }

            return percent;
        }
    }
}