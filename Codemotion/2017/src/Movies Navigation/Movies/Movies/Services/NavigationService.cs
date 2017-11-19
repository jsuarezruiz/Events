using Movies.ViewModels;
using Movies.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Movies.Services
{
    public class NavigationService
    {
        private static NavigationService _instance;

        private IDictionary<Type, Type> viewModelRouting = new Dictionary<Type, Type>()
        {
            { typeof(MoviesViewModel), typeof(MoviesView) },
            { typeof(MovieDetailViewModel), typeof(MovieDetailView) }
        };

        public static NavigationService Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new NavigationService();

                return _instance;
            }
        }

        public void NavigateTo<TDestinationViewModel>(object navigationContext = null)
        {
            Type pageType = viewModelRouting[typeof(TDestinationViewModel)];
            var page = Activator.CreateInstance(pageType, navigationContext) as Page;
            Application.Current.MainPage.Navigation.PushAsync(page);
        }
    }
}
