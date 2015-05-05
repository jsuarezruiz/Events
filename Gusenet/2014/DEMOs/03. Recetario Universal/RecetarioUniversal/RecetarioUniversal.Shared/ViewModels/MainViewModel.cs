namespace RecetarioUniversal.ViewModels
{
    using System.Linq;
    using Model;
    using RecetarioUniversal.Base;
    using RecetarioUniversal.ViewModels.Base.WPAppStudio.ViewModels.Base;
    using Services.LocalData;
    using Services.Navigation;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Windows.Input;
    using Views;

    public class MainViewModel : ViewModelBase
    {
        private readonly ILocalDataService _localDataService;
        private readonly INavigationService _navigationService;
        private readonly RecipeRepository _recipeRepository;
        private RecipeDataGroup _recipeDataGroup;
        private RecipeDataItem _recipeDataItem;

        private ICommand _recipeGroupSelectedCommand;

        public MainViewModel(ILocalDataService localDataService,
           INavigationService navigationService)
        {
            _localDataService = localDataService;
            _navigationService = navigationService;

            _recipeRepository = new RecipeRepository();
        }

        public ObservableCollection<RecipeDataGroup> Recipes
        {
            get { return _recipeRepository.ItemGroups; }
        }

        public RecipeDataGroup SelectedRecipeDataGroup
        {
            get { return _recipeDataGroup; }
            set
            {
                _recipeDataGroup = value;
                _navigationService.NavigateTo<GroupDetailView>(_recipeDataGroup);
            }
        }

        public RecipeDataItem SelectedRecipeDataItem
        {
            get { return _recipeDataItem; }
            set
            {
                _recipeDataItem = value;
                _navigationService.NavigateTo<RecipeDetailView>(_recipeDataItem);
            }
        }

        public override async void OnNavigatedTo(Windows.UI.Xaml.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var recipes = await _localDataService.Load("Data\\Recipes.txt");

            var ds = new List<string>();
            RecipeDataGroup group = null;
            foreach (RecipeDataItem recipe in recipes)
            {
                if (!ds.Contains(recipe.Group.UniqueId))
                {
                    group = recipe.Group;
                    group.Items = new ObservableCollection<RecipeDataItem>();
                    ds.Add(recipe.Group.UniqueId);
                    _recipeRepository.ItemGroups.Add(group);
                }

                _recipeRepository.AssignedUserImages(recipe);
                if (group != null) group.Items.Add(recipe);
            }
        }

        public ICommand RecipeGroupSelectedCommand
        {
            get { return _recipeGroupSelectedCommand = _recipeGroupSelectedCommand ?? new DelegateCommand<string>(RecipeGroupSelected); }
        }

        public void RecipeGroupSelected(string parameter)
        {
            _recipeDataGroup = _recipeRepository.ItemGroups.FirstOrDefault(r => r.UniqueId.Equals(parameter));
            _navigationService.NavigateTo<GroupDetailView>(_recipeDataGroup);
        }
    }
}
