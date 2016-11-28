using HelloXamarinFormsXaml.Views;
using Xamarin.Forms;

namespace HelloXamarinFormsXaml
{
    public class App : Application
    {
        private static Page _mainView;

        public static Page RootPage
        {
            get { return _mainView ?? (_mainView = new MainView()); }
        }

        public App()
        {
            MainPage = RootPage;
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
