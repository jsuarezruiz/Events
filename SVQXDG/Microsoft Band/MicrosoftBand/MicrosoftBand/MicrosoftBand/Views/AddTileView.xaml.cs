namespace MicrosoftBand.Views
{
    using Base;

    public partial class AddTileView : BaseContentPage
    {
        public AddTileView(object parameter)
        {
            InitializeComponent();

            Parameter = parameter;

            ViewModel = App.Locator.AddTileViewModel;
        }
    }
}