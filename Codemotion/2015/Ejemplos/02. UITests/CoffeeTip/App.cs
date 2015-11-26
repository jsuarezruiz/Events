using System;

using Xamarin.Forms;
using CoffeeTip.View;

namespace CoffeeTip
{
    public class App : Application
    {
        public App()
        {
            // The root page of your application
            MainPage = new NavigationPage(new TipPage())
                {
                    BarBackgroundColor = Color.FromHex("#03A9F4"),
                    BarTextColor = Color.White
                };
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

