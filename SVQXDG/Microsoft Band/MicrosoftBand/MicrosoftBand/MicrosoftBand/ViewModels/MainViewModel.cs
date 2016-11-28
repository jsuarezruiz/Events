namespace MicrosoftBand.ViewModels
{
    using Microsoft.Band.Portable;
    using Base;
    using System.Windows.Input;
    using Services.Navigation;

    public class MainViewModel : ViewModelBase
    {
        // Variables
        private string _bandName;
        private bool _isConnected;
        private string _firmwareVersion;
        private string _hardwareVersion;

        // Commands
        private ICommand _sensorsCommand;
        private ICommand _tilesCommand;
        private ICommand _vibrationsCommand;
        private ICommand _customizationCommand;

        // Services
        private INavigationService _navigationService;

        public MainViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public string BandName
        {
            get { return _bandName; }
            set
            {
                _bandName = value;
                RaisePropertyChanged();
            }
        }

        public bool IsConnected
        {
            get { return _isConnected; }
            set
            {
                _isConnected = value;
                RaisePropertyChanged();
            }
        }

        public string FirmwareVersion
        {
            get { return _firmwareVersion; }
            set
            {
                _firmwareVersion = value;
                RaisePropertyChanged();
            }
        }

        public string HardwareVersion
        {
            get { return _hardwareVersion; }
            set
            {
                _hardwareVersion = value;
                RaisePropertyChanged();
            }
        }

        public override async void OnAppearing(object navigationContext)
        {
            var bandInfo = navigationContext as BandDeviceInfo;

            if (bandInfo != null)
            {
                BandInfo = bandInfo;
                BandName = bandInfo.Name;

                if (BandClient == null)
                {
                    BandClient = await BandClientManager.Instance.ConnectAsync(BandInfo);
                }

                IsConnected = BandClient.IsConnected;
                FirmwareVersion = await BandClient.GetFirmwareVersionAsync();
                HardwareVersion = await BandClient.GetHardwareVersionAsync();
            }

            base.OnAppearing(navigationContext);
        }

        public ICommand SensorsCommand
        {
            get { return _sensorsCommand = _sensorsCommand ?? new DelegateCommand(SensorsCommandExecute); }
        }

        public ICommand TilesCommand
        {
            get { return _tilesCommand = _tilesCommand ?? new DelegateCommand(TilesCommandExecute); }
        }

        public ICommand VibrationsCommand
        {
            get { return _vibrationsCommand = _vibrationsCommand ?? new DelegateCommand(VibrationsCommandExecute); }
        }

        public ICommand CustomizationCommand
        {
            get { return _customizationCommand = _customizationCommand ?? new DelegateCommand(CustomizationCommandExecute); }
        }

        public void SensorsCommandExecute()
        {
            _navigationService.NavigateTo<SensorsViewModel>(BandClient);
        }

        public void TilesCommandExecute()
        {
            _navigationService.NavigateTo<TilesViewModel>(BandClient);
        }

        public void VibrationsCommandExecute()
        {
            _navigationService.NavigateTo<VibrationsViewModel>(BandClient);
        }

        public void CustomizationCommandExecute()
        {
            _navigationService.NavigateTo<CustomizationViewModel>(BandClient);
        }
    }
}