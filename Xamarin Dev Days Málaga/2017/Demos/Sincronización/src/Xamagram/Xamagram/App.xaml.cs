using Xamagram.Services;
using Xamagram.Views;
using Xamarin.Forms;

namespace Xamagram
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new XamagramItemsView(null));
        }

        protected override async void OnStart()
        {
            await XamagramMobileService.Instance.InitializeAsync();
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
