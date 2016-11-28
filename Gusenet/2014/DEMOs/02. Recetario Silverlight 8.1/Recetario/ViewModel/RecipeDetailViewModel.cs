using System;
using System.Linq;
using System.Windows.Input;
using Recetario.Model;
using Recetario.Services.Interfaces;
using Recetario.Services.Navigation;
using Recetario.Services.Reminder;
using Recetario.Services.Share;
using Recetario.Services.Speech;
using Recetario.Services.Tile;
using Recetario.ViewModel.Base;

namespace Recetario.ViewModel
{
    public class RecipeDetailViewModel : BindableBase
    {
        private readonly IDialogService _dialogService;
        private readonly ILiveTileServiceWP8 _liveTileService;
        private readonly INavigationService _navigationService;
        private readonly IReminderService _reminderService;
        private readonly IShareService _shareService;
        private readonly ISpeechService _speechService;
        private RecipeDataItem _recipeItem;

        private ICommand _pinToStartCommand;
        private ICommand _shareCommand;
        private ICommand _speechIngredientsCommand;
        private ICommand _speechRecipeCommand;
        private ICommand _startCookingCommand;

        public RecipeDataItem RecipeItem
        {
            get { return _recipeItem; }
            set { _recipeItem = value; }
        }

        public RecipeDetailViewModel(ILiveTileServiceWP8 tileService, IDialogService dialogService,
            INavigationService navigationService,
            IReminderService reminderService, IShareService shareService, ISpeechService speechService)
        {
            _liveTileService = tileService;
            _dialogService = dialogService;
            _navigationService = navigationService;
            _reminderService = reminderService;
            _shareService = shareService;
            _speechService = speechService;
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
            get { return _pinToStartCommand = _pinToStartCommand ?? new DelegateCommand(PinToStart); }
        }

        public ICommand SpeechRecipeCommand
        {
            get { return _speechRecipeCommand = _speechRecipeCommand ?? new DelegateCommand(SpeechRecipe); }
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
            _reminderService.SetReminder(RecipeItem);
        }

        public void Share()
        {
            _shareService.Share(RecipeItem.Title, RecipeItem.Directions);
        }

        public void PinToStart()
        {
            string uri = _navigationService.GetNavigationSource();
            if (_liveTileService.TileExists(uri))
            {
                _liveTileService.DeleteTile(uri);
                _dialogService.Show("Tile Secundario eliminado.");
            }
            else
            {
                _liveTileService.Title = _recipeItem.Title;
                _liveTileService.Count = _recipeItem.PrepTime;
                _liveTileService.BackgroundImagePath = _recipeItem.ImagePath.LocalPath;
                _liveTileService.CreateTile(uri);
            }
        }

        public void SpeechRecipe()
        {
            _speechService.TextToSpeech(RecipeItem.Directions);
        }

        public void SpeechIngredients()
        {
            string ingredients = RecipeItem.Ingredients.Aggregate(string.Empty, (current, ingredient) => current + (ingredient + Environment.NewLine));

            _speechService.TextToSpeech(ingredients);
        }
    }
}