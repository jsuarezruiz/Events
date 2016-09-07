using GeoPic.ViewModels;
using Xamarin.Forms;

namespace GeoPic.Views
{
    public partial class GeoPicDetailView : ContentPage
    {
        private object Parameter { get; set; }

        public GeoPicDetailView(object parameter)
        {
            InitializeComponent();

            Parameter = parameter;

            BindingContext = App.Locator.GeoPicDetailViewModel;
        }

        protected override void OnAppearing()
        {
            var viewModel = BindingContext as GeoPicDetailViewModel;
            if (viewModel != null) viewModel.OnAppearing(Parameter);
        }

        protected override void OnDisappearing()
        {
            var viewModel = BindingContext as GeoPicDetailViewModel;
            if (viewModel != null) viewModel.OnDisappearing();
        }
    }
}
