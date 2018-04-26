using Plugin.BatteryPlugin.Abstractions;

namespace Plugin.BatteryPlugin
{
    public class BatteryImplementation : BaseBatteryImplementation
    {
        public override int GetBatteryStatus()
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