namespace MicrosoftBand.Views.Base
{
    using ViewModels.Base;
    using Xamarin.Forms;

    public class BaseContentPage : ContentPage
    {
        protected ViewModelBase ViewModel;

        protected object Parameter { get; set; }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            BindingContext = ViewModel;
            ViewModel.OnAppearing(Parameter);
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            ViewModel.OnDisappearing();
        }
    }
}
