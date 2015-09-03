namespace MicrosoftBand.Views
{
    using Base;

    public partial class ConnectionView : BaseContentPage
    {
        public ConnectionView()
        {
            InitializeComponent();

            ViewModel = App.Locator.ConnectionViewModel;
        }
    }
}
