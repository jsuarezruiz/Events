namespace MicrosoftBand.ViewModels
{
    using Base;
    using Microsoft.Band.Portable;
    using Microsoft.Band.Portable.Personalization;
    using Helpers;
    using System.Windows.Input;
    using System.Threading.Tasks;

    public class CustomizationViewModel : ViewModelBase
    {
        // Variables
        private BandPersonalizationManager _personalizationManager;
        private BandColor _base;
        private BandColor _highContrast;
        private BandColor _highlight;
        private BandColor _lowlight;
        private BandColor _muted;
        private BandColor _secondaryText;
        private BandImage _meTileImage; 

        // Commands
        private ICommand _setMyTilemmand;
        private ICommand _setThemeCommand;

        public BandColor Base
        {
            get { return _base; }
            set
            {
                _base = value;
                RaisePropertyChanged();
            }
        }

        public BandColor HighContrast
        {
            get { return _highContrast; }
            set
            {
                _highContrast = value;
                RaisePropertyChanged();
            }
        }

        public BandColor Highlight
        {
            get { return _highlight; }
            set
            {
                _highlight = value;
                RaisePropertyChanged();
            }
        }

        public BandColor Lowlight
        {
            get { return _lowlight; }
            set
            {
                _lowlight = value;
                RaisePropertyChanged();
            }
        }

        public BandColor Muted
        {
            get { return _muted; }
            set
            {
                _muted = value;
                RaisePropertyChanged();
            }
        }

        public BandColor SecondaryText
        {
            get { return _secondaryText; }
            set
            {
                _secondaryText = value;
                RaisePropertyChanged();
            }
        }

        public BandImage MeTileImage
        {
            get { return _meTileImage; }
            set
            {
                _meTileImage = value;
                RaisePropertyChanged();
            }
        }

        public override async void OnAppearing(object navigationContext)
        {
            // Init
            BandClient = navigationContext as BandClient;
            _personalizationManager = BandClient.PersonalizationManager;

            Base = new BandColor(10, 110, 20);
            HighContrast = new BandColor(20, 120, 30);
            Highlight = new BandColor(30, 130, 40);
            Lowlight = new BandColor(40, 140, 50);
            Muted = new BandColor(50, 150, 60);
            SecondaryText = new BandColor(60, 160, 70);
            MeTileImage = await ResourcesHelper.LoadBandImageFromResourceAsync("Resources/metile.png");

            base.OnAppearing(navigationContext);
        }

        public ICommand SetMeTileCommand
        {
            get { return _setMyTilemmand = _setMyTilemmand ?? new DelegateCommandAsync(SetMyTilemmandExecute); }
        }

        public ICommand SetBandThemeCommand
        {
            get { return _setThemeCommand = _setThemeCommand ?? new DelegateCommandAsync(SetThemeCommandExecute); }
        }

        private async Task SetMyTilemmandExecute()
        {
            await _personalizationManager.SetMeTileImageAsync(MeTileImage);
        }

        private async Task SetThemeCommandExecute()
        {
            await _personalizationManager.SetThemeAsync(new BandTheme
            {
                Base = Base,
                HighContrast = HighContrast,
                Highlight = Highlight,
                Lowlight = Lowlight,
                Muted = Muted,
                SecondaryText = SecondaryText
            });
        }
    }
}
