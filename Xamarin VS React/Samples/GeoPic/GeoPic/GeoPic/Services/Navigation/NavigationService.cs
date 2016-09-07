namespace GeoPic.Services.Navigation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using ViewModels;
    using Views;
    using Xamarin.Forms;

    public class NavigationService : INavigationService
    {
        private IDictionary<Type, Type> viewModelRouting = new Dictionary<Type, Type>()
        {
            { typeof(ExtendedSplashViewModel),  typeof(ExtendedSplashView) },
            { typeof(MainViewModel),  typeof(MainView) },
            { typeof(GeoPicDetailViewModel),  typeof(GeoPicDetailView) }
        };

        public void NavigateTo<TDestinationViewModel>(object navigationContext = null)
        {
            Type pageType = viewModelRouting[typeof(TDestinationViewModel)];
            var page = Activator.CreateInstance(pageType, navigationContext) as Page;

            if (page != null)
                Application.Current.MainPage.Navigation.PushAsync(page);
        }

        public void NavigateTo(Type destinationType, object navigationContext = null)
        {
            Type pageType = viewModelRouting[destinationType];
            var page = Activator.CreateInstance(pageType, navigationContext) as Page;

            if (page != null)
                Application.Current.MainPage.Navigation.PushAsync(page);
        }

        public void NavigateBack()
        {
            Application.Current.MainPage.Navigation.PopAsync();
        }

        public void RemoveFirstPageFromBackStack()
        {
            var page = Application.Current.MainPage.Navigation.NavigationStack.FirstOrDefault();

            if (page != null)
                Application.Current.MainPage.Navigation.RemovePage(page);
        }

        public void RemoveLastPageFromBackStack()
        {
            var page = Application.Current.MainPage.Navigation.NavigationStack.LastOrDefault();

            if (page != null)
                Application.Current.MainPage.Navigation.RemovePage(page);
        }
    }
}