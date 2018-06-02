using WeatherApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WeatherApp.Views
{
    public partial class WeatherView : ContentPage
    {
        public WeatherView()
        {
            InitializeComponent();

            BindingContext = new WeatherViewModel();
        }

        protected override async void OnAppearing()
        {
            if (BindingContext is WeatherViewModel)
            {
                await ((WeatherViewModel)BindingContext).GetWeatherAsync();
            }

            base.OnAppearing();
        }
    }
}