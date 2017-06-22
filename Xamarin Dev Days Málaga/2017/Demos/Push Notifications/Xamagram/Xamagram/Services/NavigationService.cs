using System;
using System.Collections.Generic;
using Xamagram.ViewModels;
using Xamagram.Views;
using Xamarin.Forms;

namespace Xamagram.Services
{
    public class NavigationService
    {
        private static NavigationService _instance;
        private IDictionary<Type, Type> viewModelRouting = new Dictionary<Type, Type>()
        {
            { typeof(NewXamagramItemViewModel), typeof(NewXamagramItemView) },
            { typeof(XamagramItemsViewModel), typeof(XamagramItemsView) },
            { typeof(XamagramItemDetailViewModel), typeof(XamagramItemDetailView) }
        };

        public static NavigationService Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new NavigationService();
                }

                return _instance;
            }
        }

        public void NavigateTo<TDestinationViewModel>(object navigationContext = null)
        {
            Type pageType = viewModelRouting[typeof(TDestinationViewModel)];
            var page = Activator.CreateInstance(pageType, navigationContext) as Page;

            if (page != null)
                Application.Current.MainPage.Navigation.PushAsync(page);
        }

        public void NavigateBack()
        {
            Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}
