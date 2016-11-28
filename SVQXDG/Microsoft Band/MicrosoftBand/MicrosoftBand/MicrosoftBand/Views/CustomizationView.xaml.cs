namespace MicrosoftBand.Views
{
    using Base;

    public partial class CustomizationView : BaseContentPage
    {
        public CustomizationView(object parameter)
        {
            InitializeComponent();

            Parameter = parameter;

            ViewModel = App.Locator.CustomizationViewModel;
        }
    }
}
