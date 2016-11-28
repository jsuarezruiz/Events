using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Recetario.Model;
using Recetario.Services.Interfaces;
using Recetario.Services.Navigation;
using Recetario.Services.Storage;
using Recetario.Services.Tile;
using Recetario.ViewModel.Base;

namespace Recetario.ViewModel
{
    public class MainViewModel : BindableBase
    {
        private readonly ILiveTileServiceWP8 _liveTileService;
        private readonly ILocalDataService _localDataService;
        private readonly INavigationService _navigationService;
        private readonly IStorageService _storageService;
        private readonly RecipeRepository _recipeRepository = new RecipeRepository();
        private RecipeDataGroup _recipeDataGroup;

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
                _navigationService.NavigateToGroupDetailPage(_recipeDataGroup);
            }
        }

        public MainViewModel(ILocalDataService localDataService, IStorageService storageService,
            INavigationService navigationService,
            ILiveTileServiceWP8 liveTileService)
        {
            _localDataService = localDataService;
            _storageService = storageService;
            _navigationService = navigationService;
            _liveTileService = liveTileService;
        }

        // Carga la colección de recetas
        public void LoadRecipes()
        {
            IEnumerable<RecipeDataItem> recipes =
                new ObservableCollection<RecipeDataItem>(
                    (_localDataService.Load<RecipeDataItem>("Data\\Recipes.txt").ToList()));

            var IDs = new List<string>();
            RecipeDataGroup group = null;
            foreach (RecipeDataItem recipe in recipes)
            {
                if (!IDs.Contains(recipe.Group.UniqueId))
                {
                    group = recipe.Group;
                    group.Items = new ObservableCollection<RecipeDataItem>();
                    IDs.Add(recipe.Group.UniqueId);
                    _recipeRepository.ItemGroups.Add(group);
                }

                _recipeRepository.AssignedUserImages(recipe);
                group.Items.Add(recipe);
            }
        }

        // Guarda la colección de cartas a disco
        public void SaveRecipesToIs()
        {
            _storageService.Save("RecipesFile",
                _recipeRepository.ItemGroups
                );
        }

        public void UpdateTile()
        {
            // Título
            _liveTileService.Title = "Recetario";
            _liveTileService.BackTitle = "WP7";

            // Contador
            _liveTileService.Count = _recipeRepository.ItemGroups.Count;

            // Texto
            _liveTileService.BackContent = "SecondNug";

            // Actualiza el live tile
            _liveTileService.UpdateTile();
        }
    }
}