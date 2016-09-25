using Windows.UI.Xaml.Controls;
using Xbox_Gamepad.ViewModels;

namespace Xbox_Gamepad.Views
{
    public sealed partial class MainView : Page
    {
        public MainView()
        {
            this.InitializeComponent();

            KeyUp += OnKeyUp;
        }

        private void OnKeyUp(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
            var vm = DataContext as MainViewModel;

            if (vm != null)
            {
                vm.Key = e.Key.ToString();
            }
        }
    }
}