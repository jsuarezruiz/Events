namespace MicrosoftBand.ViewModels
{
    using Base;
    using Microsoft.Band.Portable;
    using Services.Navigation;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;

    public class ConnectionViewModel : ViewModelBase
    {
        // Variables
        private ObservableCollection<BandDeviceInfo> _bands;
        private BandDeviceInfo _band;

        // Services
        private INavigationService _navigationService;

        public ConnectionViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public ObservableCollection<BandDeviceInfo> Bands
        {
            get { return _bands; }
            set
            {
                _bands = value;
                RaisePropertyChanged();
            }
        }

        public BandDeviceInfo Band
        {
            get { return _band; }
            set
            {
                _band = value;

                // Navigate to MainView. Pass BandDeviceInfo as parameter
                _navigationService.NavigateTo<MainViewModel>(_band);
            }
        }

        public override async void OnAppearing(object navigationContext)
        {
            await LoadBands();
        }

        private async Task LoadBands()
        {
            Bands = new ObservableCollection<BandDeviceInfo>();

            // Get paired Bands
            IEnumerable<BandDeviceInfo> bands = await BandClientManager.Instance.GetPairedBandsAsync();
            foreach (var band in bands)
            {
                Bands.Add(band);
            }
        }
    }
}
