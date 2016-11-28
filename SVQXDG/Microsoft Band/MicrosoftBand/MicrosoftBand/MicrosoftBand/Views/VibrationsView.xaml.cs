namespace MicrosoftBand.Views
{
    using Base;

    public partial class VibrationsView : BaseContentPage
    {
        public VibrationsView(object parameter)
        {
            InitializeComponent();

            Parameter = parameter;

            ViewModel = App.Locator.VibrationsViewModel;

            foreach (var type in App.Locator.VibrationsViewModel.VibrationTypes)
            {
                VibrationTypesPicker.Items.Add(type);
            }
        }
    }
}
