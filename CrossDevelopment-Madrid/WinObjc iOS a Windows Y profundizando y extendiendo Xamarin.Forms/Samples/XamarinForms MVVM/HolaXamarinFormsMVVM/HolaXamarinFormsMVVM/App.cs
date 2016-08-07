using HolaXamarinFormsMVVM.ViewModels.Base;
using HolaXamarinFormsMVVM.Views;
using System.Diagnostics;
using Xamarin.Forms;

namespace HolaXamarinFormsMVVM
{
    public class App : Application
    {
        private static ViewModelLocator _locator;

        public static ViewModelLocator Locator
        {
            get { return _locator = _locator ?? new ViewModelLocator(); }
        }

        public App()
        {
            MainPage = new MainView();
        }

        protected override void OnStart()
        {
            Debug.WriteLine("OnStart");
        }

        protected override void OnSleep()
        {
            Debug.WriteLine("OnSleep");
        }

        protected override void OnResume()
        {
            Debug.WriteLine("OnResume");
        }
    }
}
