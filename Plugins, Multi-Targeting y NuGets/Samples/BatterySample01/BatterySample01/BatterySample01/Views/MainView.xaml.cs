using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BatterySample01.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MainView : ContentPage
	{
		public MainView ()
		{
			InitializeComponent ();

            GetBatteryLevelBtn.Clicked += (sender, args) =>
            {
#if __ANDROID__
                try
                {
                    using (var filter = new Android.Content.IntentFilter(Android.Content.Intent.ActionBatteryChanged))
                    {
                        using (var battery = Android.App.Application.Context.RegisterReceiver(null, filter))
                        {
                            var level = battery.GetIntExtra(Android.OS.BatteryManager.ExtraLevel, -1);
                            var scale = battery.GetIntExtra(Android.OS.BatteryManager.ExtraScale, -1);

                            BatteryLevelLabel.Text = string.Format("{0} %", (int)Math.Floor(level * 100D / scale));
                        }
                    }
                }
                catch
                {
                    BatteryLevelLabel.Text = "Unable to gather battery level, ensure you have android.permission.BATTERY_STATS set in AndroidManifest.";
                }
#else
#if __IOS__
                try
                {
                    BatteryLevelLabel.Text = string.Format("{0} %", (int)(UIKit.UIDevice.CurrentDevice.BatteryLevel * 100F));
                }
                catch
                {
                       BatteryLevelLabel.Text = "Unable to gather battery level";
                }
#else
                // UWP
                var battery = Windows.Devices.Power.Battery.AggregateBattery;
                var finalReport = battery.GetReport();
                var finalPercent = -1;

                if (finalReport.RemainingCapacityInMilliwattHours.HasValue && finalReport.FullChargeCapacityInMilliwattHours.HasValue)
                {
                    finalPercent = (int)((finalReport.RemainingCapacityInMilliwattHours.Value /
                                     (double)finalReport.FullChargeCapacityInMilliwattHours.Value) * 100);
                }

                BatteryLevelLabel.Text = string.Format("{0} %", finalPercent);
#endif
#endif
            };
        }
	}
}