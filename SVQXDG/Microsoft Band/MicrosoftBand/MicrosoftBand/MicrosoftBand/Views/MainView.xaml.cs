namespace MicrosoftBand.Views
{
    using Base;

    public partial class MainView : BaseContentPage
    {

        public MainView(object parameter)
        {
            InitializeComponent();

            Parameter = parameter;

            ViewModel = App.Locator.MainViewModel;
        }
    }
}
