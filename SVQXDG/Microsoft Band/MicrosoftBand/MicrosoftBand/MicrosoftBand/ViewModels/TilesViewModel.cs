namespace MicrosoftBand.ViewModels
{
    using Base;
    using Microsoft.Band.Portable;
    using Microsoft.Band.Portable.Tiles;
    using Models;
    using Services.Navigation;
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;
    using System.Windows.Input;

    public class TilesViewModel : ViewModelBase
    {
        // Variables
        private BandTileManager _tileManager;
        private int _remainingCapacity;
        private ObservableCollection<BandTile> _tiles;
        private BandTile _tile;

        // Commands
        private ICommand _addTileCommand;

        // Services
        private INavigationService _navigationService;

        public TilesViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }


        public int RemainingCapacity
        {
            get { return _remainingCapacity; }
            set
            {
                _remainingCapacity = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<BandTile> Tiles
        {
            get { return _tiles; }
            set
            {
                _tiles = value;
                RaisePropertyChanged();
            }
        }

        public BandTile Tile
        {
            get { return _tile; }
            set
            {
                _tile = value;

                var notificationData = new NotificationData
                {
                    BandClient = BandClient,
                    Tile = _tile
                };

                _navigationService.NavigateTo<NotificationsViewModel>(notificationData);
            }
        }

        public override async void OnAppearing(object navigationContext)
        {
            BandClient = navigationContext as BandClient;
            _tileManager = BandClient.TileManager;

            // Load Remaining CApacity
            RemainingCapacity = await _tileManager.GetRemainingTileCapacityAsync();

            // Load Tiles
            await LoadTiles();

            base.OnAppearing(navigationContext);
        }

        private async Task LoadTiles()
        {
            var tiles = await _tileManager.GetTilesAsync();
            Tiles = new ObservableCollection<BandTile>(tiles);
        }

        public ICommand AddTileCommand
        {
            get { return _addTileCommand = _addTileCommand ?? new DelegateCommand(AddTileCommandExecute); }
        }

        private void AddTileCommandExecute()
        {
            _navigationService.NavigateTo<AddTileViewModel>(BandClient);
        }
    }
}
