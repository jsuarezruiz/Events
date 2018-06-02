using Xamarin.Forms.Platform.WPF;

namespace WeatherApp.WPF
{
    public partial class MainWindow : FormsApplicationPage
    {
        public MainWindow()
        {
            InitializeComponent();
            Xamarin.Forms.Forms.Init();
            LoadApplication(new WeatherApp.App());
        }
    }
}