using DevDaysWorkshop.ViewModels;
using Xamarin.Forms;

namespace DevDaysWorkshop.Views
{
    public partial class SpeakerDetailView : ContentPage
    {
        public SpeakerDetailView(object parameter)
        {
            InitializeComponent();

            Parameter = parameter;

            BindingContext = new SpeakerDetailViewModel();
        }

        public object Parameter { get; set; }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var viewModel = BindingContext as SpeakerDetailViewModel;
            if (viewModel != null) viewModel.OnAppearing(Parameter);
        }
    }
}