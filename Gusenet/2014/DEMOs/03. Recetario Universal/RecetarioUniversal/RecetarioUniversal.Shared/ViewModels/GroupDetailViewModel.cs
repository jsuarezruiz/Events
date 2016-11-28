namespace RecetarioUniversal.ViewModels
{
    using RecetarioUniversal.Base;
    using Model;
    using Services.Navigation;
    using Views;
    using Windows.UI.Xaml.Navigation;
    using System.Windows.Input;
    using Base.WPAppStudio.ViewModels.Base;

    public class GroupDetailViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;

        private RecipeDataItem _selectedRecipe;

        private ICommand _goBackCommand;

        public GroupDetailViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public RecipeDataItem SelectedRecipe
        {
            get { return _selectedRecipe; }
            set
            {
                _selectedRecipe = value;
                _navigationService.NavigateTo<RecipeDetailView>(_selectedRecipe);
            }
        }

        public RecipeDataGroup RecipeGroup { get; set; }

        public override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            RecipeGroup = e.Parameter as RecipeDataGroup;
        }

        public ICommand GoBackCommand
        {
            get { return _goBackCommand = _goBackCommand ?? new DelegateCommand(GoBack); }
        }

        public void GoBack()
        {
            _navigationService.NavigateBack();
        }
    }
}
