namespace MicrosoftBand.Views
{
    using Base;

    public partial class SensorsView : BaseContentPage
    {
        public SensorsView(object parameter)
        {
            InitializeComponent();

            Parameter = parameter;

            ViewModel = App.Locator.SensorsViewModel;
        }
    }
}