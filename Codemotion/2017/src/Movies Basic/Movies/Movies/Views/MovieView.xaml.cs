using Movies.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Movies.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MovieView : ContentPage
    {
        public MovieView()
        {
            InitializeComponent();

            BindingContext = new MoviesViewModel();
        }
    }
}