using Xamagram.ViewModels;
using Xamarin.Forms;

namespace Xamagram.Views
{
    public partial class NewXamagramItemView : ContentPage
    {
        public NewXamagramItemView(object parameter)
        {
            InitializeComponent();

            Parameter = parameter;

            BindingContext = new NewXamagramItemViewModel();
        }

        public object Parameter { get; set; }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var viewModel = BindingContext as NewXamagramItemViewModel;
            if (viewModel != null) viewModel.OnAppearing(Parameter);
        }
    }
}

