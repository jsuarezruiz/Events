namespace MicrosoftBand.ViewModels
{
    using Base;
    using Microsoft.Band.Portable;
    using Microsoft.Band.Portable.Notifications;
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Input;

    public class VibrationsViewModel : ViewModelBase
    {
        // Variables
        private BandNotificationManager _notifiactionManager;
        private ObservableCollection<string> _vibrationTypes;
        private int _vibrationIndex;

        // Commands
        private ICommand _vibrateCommand;

        public VibrationsViewModel()
        {
            VibrationTypes = GetVibrationTypes();
        }

        public ObservableCollection<string> VibrationTypes
        {
            get { return _vibrationTypes; }
            set
            {
                _vibrationTypes = value;
                RaisePropertyChanged();
            }
        }

        public int VibrationIndex
        {
            get { return _vibrationIndex; }
            set
            {
                _vibrationIndex = value;
                RaisePropertyChanged();
            }
        }

        public override void OnAppearing(object navigationContext)
        {
            BandClient = navigationContext as BandClient;
            _notifiactionManager = BandClient.NotificationManager;

            base.OnAppearing(navigationContext);
        }

        public ICommand VibrateCommand
        {
            get { return _vibrateCommand = _vibrateCommand ?? new DelegateCommandAsync(VibrateCommandExecute); }
        }

        private async Task VibrateCommandExecute()
        {
            await _notifiactionManager.VibrateAsync((VibrationType)_vibrationIndex);
        }

        public ObservableCollection<string> GetVibrationTypes()
        {
            var names = Enum.GetNames(typeof(VibrationType));
            var split = names.Select(n =>
                string.Concat(n.ToCharArray().Select(c =>
                    char.IsUpper(c) ? " " + c : c.ToString())));
            return new ObservableCollection<string>(split.ToList());
        }
    }
}