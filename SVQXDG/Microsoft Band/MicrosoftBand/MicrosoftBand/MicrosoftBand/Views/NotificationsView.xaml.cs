namespace MicrosoftBand.Views
{
    using Base;

    public partial class NotificationsView : BaseContentPage
    {
        public NotificationsView(object parameter)
        {
            InitializeComponent();

            Parameter = parameter;

            ViewModel = App.Locator.NotificationsViewModel;
        }
    }
}
