using dotnetspain2015.Services.Localize;
using dotnetspain2015.Views;
using Xamarin.Forms;

namespace dotnetspain2015
{
    public class App : Application
    {
        public App()
        {
            MainPage = new MainView();

            if (Device.OS != TargetPlatform.WinPhone)
                DependencyService.Get<ILocalizeService>().SetLocale();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
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
