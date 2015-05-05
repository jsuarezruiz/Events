using System;
using System.Windows;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Recetario.Model;
using Recetario.View;

namespace Recetario.Services.Navigation
{
    public class NavigationService : INavigationService
    {
        private RecipeDataGroup _selectedRecipeDataGroup;
        private RecipeDataItem _selectedRecipeDataItem;

        public string GetNavigationSource()
        {
            var phoneApplicationFrame = Application.Current.RootVisual as PhoneApplicationFrame;
            if (phoneApplicationFrame != null)
                return phoneApplicationFrame.Source.ToString();

            return null;
        }

        public void NavigateToGroupDetailPage(RecipeDataGroup recipeDataGroup)
        {
            _selectedRecipeDataGroup = recipeDataGroup;
            var phoneApplicationFrame = Application.Current.RootVisual as PhoneApplicationFrame;
            if (phoneApplicationFrame != null)
                phoneApplicationFrame.Navigated += NavigateToGroupDetailPageNavigated;
            App.RootFrame.Navigate(new Uri("/View/GroupDetailPage.xaml", UriKind.Relative));
        }

        public void NavigateToRecipeDetailPage(RecipeDataItem recipeDataItem)
        {
            _selectedRecipeDataItem = recipeDataItem;
            var phoneApplicationFrame = Application.Current.RootVisual as PhoneApplicationFrame;
            if (phoneApplicationFrame != null)
                phoneApplicationFrame.Navigated += NavigateToRecipeDetailPageNavigated;
            App.RootFrame.Navigate(new Uri("/View/RecipeDetailPage.xaml", UriKind.Relative));
        }

        private void NavigateToGroupDetailPageNavigated(object sender, NavigationEventArgs e)
        {
            var phoneApplicationFrame = Application.Current.RootVisual as PhoneApplicationFrame;
            if (phoneApplicationFrame != null)
                phoneApplicationFrame.Navigated -= NavigateToGroupDetailPageNavigated;

            if (e.Content.GetType() == typeof (GroupDetailPage))
            {
                var groupDetailPage = e.Content as GroupDetailPage;
                if (groupDetailPage != null)
                    groupDetailPage.RecipeGroup = _selectedRecipeDataGroup;
            }
        }

        private void NavigateToRecipeDetailPageNavigated(object sender, NavigationEventArgs e)
        {
            var phoneApplicationFrame = Application.Current.RootVisual as PhoneApplicationFrame;
            if (phoneApplicationFrame != null)
                phoneApplicationFrame.Navigated -= NavigateToRecipeDetailPageNavigated;

            if (e.Content.GetType() == typeof (RecipeDetailPage))
            {
                var recipeDetailPage = e.Content as RecipeDetailPage;
                if (recipeDetailPage != null)
                    recipeDetailPage.RecipeItem = _selectedRecipeDataItem;
            }
        }
    }
}