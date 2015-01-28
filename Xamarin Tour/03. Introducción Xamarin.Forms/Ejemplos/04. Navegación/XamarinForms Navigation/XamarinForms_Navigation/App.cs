using Xamarin.Forms;
using XamarinForms_Navigation.Views;

namespace XamarinForms_Navigation
{
    public class App : Application
    {
        public App()
        {
            MainPage = new FirstView();
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
