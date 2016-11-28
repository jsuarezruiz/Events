using Xamarin.Forms;
using Xamarin.Forms_Services.ViewModels;

namespace Xamarin.Forms_Services.Views
{
    public partial class MainView : ContentPage
    {
        public MainView()
        {
            InitializeComponent();

            BindingContext = new MainViewModel();
        }
    }
}
