using Movies.Web.ViewModels;
using Movies.Web.ViewModels.Base;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Movies.Web.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomeView : ContentPage
    {
        public HomeView()
        {
            InitializeComponent();

            ViewModelBase viewModel = Locator.Instance.Resolve<HomeViewModel>() as ViewModelBase;

            BindingContext = viewModel;
        }

        protected override async void OnAppearing()
        {
            await (BindingContext as ViewModelBase).InitializeAsync(null);
        }
    }
}