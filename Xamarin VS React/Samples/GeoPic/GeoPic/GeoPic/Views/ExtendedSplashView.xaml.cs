using GeoPic.ViewModels;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GeoPic.Views
{
    public partial class ExtendedSplashView : ContentPage
    {
        private object Parameter { get; set; }

        public ExtendedSplashView(object parameter)
        {
            InitializeComponent();

            Parameter = parameter;

            BindingContext = App.Locator.ExtendedSplashViewModel;
        }

        protected override async void OnAppearing()
        {
            var viewModel = BindingContext as ExtendedSplashViewModel;
            if (viewModel != null) viewModel.OnAppearing(Parameter);

            await Animate();
        }

        protected override void OnDisappearing()
        {
            var viewModel = BindingContext as ExtendedSplashViewModel;
            if (viewModel != null) viewModel.OnDisappearing();
        }

        private async Task Animate()
        {
            await Task.Delay(2500);
            await Logo.ScaleTo(5, length: 50, easing: Easing.CubicIn);
        }
    }
}
