using TemperatureMonitor.ViewModels;
using Xamarin.Forms;

namespace TemperatureMonitor.Views
{
    public partial class TemperatureMonitorView : ContentPage
    {
        public TemperatureMonitorView(object parameter)
        {
            InitializeComponent();

            Parameter = parameter;

            BindingContext = App.Locator.TemperatureMonitorViewModel;
        }

        public object Parameter { get; set; }

        protected override void OnAppearing()
        {
            var viewModel = BindingContext as TemperatureMonitorViewModel;
            if (viewModel != null) viewModel.OnAppearing(Parameter);
        }

        protected override void OnDisappearing()
        {
            var viewModel = BindingContext as TemperatureMonitorViewModel;
            if (viewModel != null) viewModel.OnDisappearing();
        }
    }
}