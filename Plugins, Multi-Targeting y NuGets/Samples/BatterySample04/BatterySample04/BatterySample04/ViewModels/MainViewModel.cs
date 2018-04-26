using Plugin.BatteryPlugin;
using System.Windows.Input;
using Xamarin.Forms;

namespace BatterySample04.ViewModels
{
    public class MainViewModel : BindableObject
    {
        private string _batteryStatus;

        public string BatteryStatus
        {
            get { return _batteryStatus; }
            set
            {
                _batteryStatus = value;
                OnPropertyChanged();
            }
        }

        public ICommand GetBatteryStatusCommand => new Command(GetBatteryStatus);

        private void GetBatteryStatus()
        {
            var batteryStatus = CrossBattery.Current.GetBatteryStatus();

            BatteryStatus = string.Format("{0} %", batteryStatus);
        }
    }
}