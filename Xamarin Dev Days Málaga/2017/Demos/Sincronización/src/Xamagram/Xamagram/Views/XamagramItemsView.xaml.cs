using Xamagram.ViewModels;
using Xamarin.Forms;

namespace Xamagram.Views
{
    public partial class XamagramItemsView : ContentPage
    {
        public XamagramItemsView(object parameter)
        {
            InitializeComponent();

            Parameter = parameter;

            BindingContext = new XamagramItemsViewModel(); 
        }

        public object Parameter { get; set; }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var viewModel = BindingContext as XamagramItemsViewModel;
            if (viewModel != null) viewModel.OnAppearing(Parameter);
        }
    }
}
