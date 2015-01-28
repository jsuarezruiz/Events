using Montaditos.ViewModels;
using Xamarin.Forms;

namespace Montaditos.Views
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
