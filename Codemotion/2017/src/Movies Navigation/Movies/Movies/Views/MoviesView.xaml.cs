using Movies.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Movies.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MoviesView : ContentPage
    {
        public MoviesView()
        {
            InitializeComponent();

            BindingContext = new MoviesViewModel();
        }
    }
}