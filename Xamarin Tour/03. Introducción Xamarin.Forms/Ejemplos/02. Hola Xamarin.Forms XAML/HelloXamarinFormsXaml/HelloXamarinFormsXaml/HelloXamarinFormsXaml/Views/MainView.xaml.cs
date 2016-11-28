using HelloXamarinFormsXaml.ViewModels;
using Xamarin.Forms;

namespace HelloXamarinFormsXaml.Views
{
    public partial class MainView : ContentPage
    {
        public MainView()
        {
            InitializeComponent();

            // DataContext de la página, nuestro ViewModel
            BindingContext = new MainViewModel();
        }
    }
}
