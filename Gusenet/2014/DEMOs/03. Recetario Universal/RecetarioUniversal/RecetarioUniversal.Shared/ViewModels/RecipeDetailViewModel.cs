using RecetarioUniversal.Services.Navigation;

namespace RecetarioUniversal.ViewModels
{
    using Model;
    using Services.Dialog;
    using Services.Share;
    using Services.Speech;
    using Services.Tile;
    using Base;
    using Base.WPAppStudio.ViewModels.Base;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using RecetarioUniversal.Base;
    using Windows.UI.Xaml.Navigation;

    public class RecipeDetailViewModel : ViewModelBase
    {
        private readonly IDialogService _dialogService;
        private readonly ITileService _liveTileService;
        private readonly IShareService _shareService;
        private readonly ISpeechService _speechService;
        private readonly INavigationService _navigationService;

        private RecipeDataItem _recipeItem;

        private ICommand _pinToStartCommand;
        private ICommand _shareCommand;
        private ICommand _speechIngredientsCommand;
        private ICommand _speechRecipeCommand;
        private ICommand _startCookingCommand;
        private ICommand _goBackCommand;

        public RecipeDataItem RecipeItem
        {
            get { return _recipeItem; }
            set { _recipeItem = value; }
        }

        public RecipeDetailViewModel(ITileService tileService, IDialogService dialogService,
            IShareService shareService, ISpeechService speechService, INavigationService navigationService)
        {
            _liveTileService = tileService;
            _dialogService = dialogService;
            _shareService = shareService;
            _speechService = speechService;
            _navigationService = navigationService;
        }

        public override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            RecipeItem = e.Parameter as RecipeDataItem;
        }

        public ICommand StartCookingCommand
        {
            get { return _startCookingCommand = _startCookingCommand ?? new DelegateCommand(StartCooking); }
        }

        public ICommand ShareCommand
        {
            get { return _shareCommand = _shareCommand ?? new DelegateCommand(Share); }
        }

        public ICommand PinToStartCommand
        {
            get { return _pinToStartCommand = _pinToStartCommand ?? new DelegateCommandAsync(PinToStart); }
        }

        public ICommand SpeechRecipeCommand
        {
            get { return _speechRecipeCommand = _speechRecipeCommand ?? new DelegateCommand(SpeechRecipe); }
        }

        public ICommand GoBackCommand
        {
            get { return _goBackCommand = _goBackCommand ?? new DelegateCommand(GoBack); }
        }

        public ICommand SpeechIngredientsCommand
        {
            get
            {
                return _speechIngredientsCommand = _speechIngredientsCommand ?? new DelegateCommand(SpeechIngredients);
            }
        }

        public void StartCooking()
        {
            throw new NotImplementedException("No disponible en Universal Apps!");
        }

        public void Share()
        {
            _shareService.Share(RecipeItem.Title, RecipeItem.Directions);
        }

        public async Task PinToStart()
        {
            const string tileId = "RecipeDetailView";
            if (_liveTileService.SecondaryTileExists(tileId))
            {
                await _liveTileService.UnpinTile(tileId);
                await _dialogService.Show("Tile Secundario eliminado.");
            }
            else
                await _liveTileService.PinToStart(tileId, _recipeItem.Title, _recipeItem.ImagePath, string.Empty);
        }

        public void SpeechRecipe()
        {
            _speechService.TextToSpeech(RecipeItem.Directions);
        }

        public void SpeechIngredients()
        {
            var ingredients = RecipeItem.Ingredients.Aggregate(string.Empty, (current, ingredient) => current + (ingredient + Environment.NewLine));

            _speechService.TextToSpeech(ingredients);
        }

        public void GoBack()
        {
            _navigationService.NavigateBack();
        }
    }
}
