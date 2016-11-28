using Orbit.ViewModels.Base;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Orbit.Views.Base
{
    public class PageBase : Page
    {
        private ViewModelBase _vm;

        public PageBase()
        {
            SystemNavigationManager.GetForCurrentView().BackRequested += App_BackRequested;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            _vm = (ViewModelBase)this.DataContext;
            _vm.SetAppFrame(this.Frame);
            _vm.OnNavigatedTo(e);

            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = Frame.CanGoBack ?
                AppViewBackButtonVisibility.Visible : AppViewBackButtonVisibility.Collapsed;
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            _vm.OnNavigatedFrom(e);
        }

        private void App_BackRequested(object sender, BackRequestedEventArgs e)
        {
            if (e.Handled)
            {
                return;
            }

            if (Frame.CanGoBack)
                Frame.GoBack();
        }
    }
}
