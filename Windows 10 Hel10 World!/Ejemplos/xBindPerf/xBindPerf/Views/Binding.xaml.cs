using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace xBindPerf.Views
{
    public sealed partial class Binding : Page
    {
        public Binding()
        {
            this.InitializeComponent();
        }

        public SolidColorBrush BackgroundA => Application.Current.Resources["BackgroundA"] as SolidColorBrush;

        public SolidColorBrush BackgroundB => Application.Current.Resources["BackgroundB"] as SolidColorBrush;

        public SolidColorBrush BackgroundC => Application.Current.Resources["BackgroundC"] as SolidColorBrush;

        public SolidColorBrush BackgroundD => Application.Current.Resources["BackgroundD"] as SolidColorBrush;
    }
}
