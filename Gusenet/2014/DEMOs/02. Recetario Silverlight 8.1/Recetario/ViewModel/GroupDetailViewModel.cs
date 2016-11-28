using Recetario.Model;
using Recetario.Services.Navigation;
using Recetario.ViewModel.Base;

namespace Recetario.ViewModel
{
    public class GroupDetailViewModel : BindableBase
    {
        private readonly INavigationService _navigationService;
        private RecipeDataItem _selectedRecipe;

        public RecipeDataItem SelectedRecipe
        {
            get { return _selectedRecipe; }
            set
            {
                _selectedRecipe = value;
                _navigationService.NavigateToRecipeDetailPage(_selectedRecipe);
            }
        }

        public RecipeDataGroup RecipeGroup { get; set; }

        public GroupDetailViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }
    }
}