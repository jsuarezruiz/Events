using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinForms3DCarSample.Services.Navigation;
using XamarinForms3DCarSample.ViewModels.Base;

namespace XamarinForms3DCarSample
{
    public partial class App : Application
    {
        public App()
        {
            InitNavigation();
        }

        public static Game Game { get; set; }

        private void InitGame()
        {
            Game = new Game();
        }

        private Task InitNavigation()
        {
            var navigationService = ViewModelLocator.Instance.Resolve<INavigationService>();
            return navigationService.InitializeAsync();
        }

        protected override void OnStart()
        {
            InitGame();
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