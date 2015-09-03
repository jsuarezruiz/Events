namespace MicrosoftBand.Views
{
    using Base;

    public partial class TilesView : BaseContentPage
    {
        public TilesView(object parameter)
        {
            InitializeComponent();

            Parameter = parameter;

            ViewModel = App.Locator.TilesViewModel;
        }
    }
}