using DevDaysWorkshop.ViewModels;
using Xamarin.Forms;

namespace DevDaysWorkshop.Views
{
    public partial class SpeakersView : ContentPage
    {
        public SpeakersView(object parameter)
        {
            InitializeComponent();

            Parameter = parameter;

            BindingContext = new SpeakersViewModel();
        }

        public object Parameter { get; set; }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var viewModel = BindingContext as SpeakersViewModel;
            if (viewModel != null) viewModel.OnAppearing(Parameter);
        }
    }
}