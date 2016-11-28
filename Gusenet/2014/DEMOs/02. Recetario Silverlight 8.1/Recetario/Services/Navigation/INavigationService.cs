using Recetario.Model;

namespace Recetario.Services.Navigation
{
    public interface INavigationService
    {
        string GetNavigationSource();
        void NavigateToGroupDetailPage(RecipeDataGroup recipeDataGroup);
        void NavigateToRecipeDetailPage(RecipeDataItem recipeDataItem);
    }
}
