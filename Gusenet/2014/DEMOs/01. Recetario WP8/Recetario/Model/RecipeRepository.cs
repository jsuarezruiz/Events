using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Recetario.Model
{
    internal class RecipeRepository
    {
        public List<UserRecipeImages> UserImages = new List<UserRecipeImages>();
        private ObservableCollection<RecipeDataGroup> _itemGroups = new ObservableCollection<RecipeDataGroup>();

        public ObservableCollection<RecipeDataGroup> ItemGroups
        {
            get { return _itemGroups; }
            set { _itemGroups = value; }
        }

        public RecipeDataGroup FindGroup(string uniqueId)
        {
            return (from g in ItemGroups where g.UniqueId == uniqueId select g).SingleOrDefault();
        }

        public RecipeDataItem FindRecipe(string uniqueId)
        {
            return ItemGroups.SelectMany(g => g.Items).SingleOrDefault(i => i.UniqueId == uniqueId);
        }

        public void AssignedUserImages(RecipeDataItem recipe)
        {
            if (UserImages != null && UserImages.Any(item => item.UniqueId.Equals(recipe.UniqueId)))
            {
                UserRecipeImages userImages = UserImages.Single(a => a.UniqueId.Equals(recipe.UniqueId));
                recipe.UserImages = userImages.Images;
            }
        }
    }
}