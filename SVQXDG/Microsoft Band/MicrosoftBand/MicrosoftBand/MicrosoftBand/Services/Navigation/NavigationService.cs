namespace MicrosoftBand.Services.Navigation
{
    using ViewModels;
    using Views;
    using System;
    using System.Collections.Generic;
    using Xamarin.Forms;

    public class NavigationService : INavigationService
    {
        private IDictionary<Type, Type> viewModelRouting = new Dictionary<Type, Type>()
        {
            { typeof(AddTileViewModel), typeof(AddTileView) },
            { typeof(ConnectionViewModel), typeof(ConnectionView) },
            { typeof(CustomizationViewModel), typeof(CustomizationView) },
            { typeof(MainViewModel), typeof(MainView) },
            { typeof(NotificationsViewModel), typeof(NotificationsView) },
            { typeof(SensorsViewModel), typeof(SensorsView) },
            { typeof(TilesViewModel), typeof(TilesView) },
            { typeof(VibrationsViewModel), typeof(VibrationsView) },
        };

        public void NavigateTo<TDestinationViewModel>(object navigationContext = null)
        {
            Type pageType = viewModelRouting[typeof(TDestinationViewModel)];
            var page = Activator.CreateInstance(pageType, navigationContext) as Page;
            Application.Current.MainPage.Navigation.PushAsync(page);
        }

        public void NavigateBack()
        {
            Application.Current.MainPage.Navigation.PopAsync();
        }

        public void NavigateBackToFirst()
        {
            Application.Current.MainPage.Navigation.PopToRootAsync();
        }
    }
}
