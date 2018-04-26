using BatterySample02.Services;
using System.Windows.Input;
using Xamarin.Forms;

namespace BatterySample02.ViewModels
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
            IBatteryService batteryService = DependencyService.Get<IBatteryService>();
            var batteryStatus = batteryService.GetBatteryStatus();

            BatteryStatus = string.Format("{0} %", batteryStatus);
        }
    }
}