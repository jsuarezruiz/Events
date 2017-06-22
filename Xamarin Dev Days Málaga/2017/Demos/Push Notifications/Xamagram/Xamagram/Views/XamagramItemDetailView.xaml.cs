using Xamagram.ViewModels;
using Xamarin.Forms;

namespace Xamagram.Views
{
    public partial class XamagramItemDetailView : ContentPage
    {
        public XamagramItemDetailView(object parameter)
        {
            InitializeComponent();

            Parameter = parameter;

            BindingContext = new XamagramItemDetailViewModel();
        }

        public object Parameter { get; set; }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var viewModel = BindingContext as XamagramItemDetailViewModel;
            if (viewModel != null) viewModel.OnAppearing(Parameter);
        }
    }
}
