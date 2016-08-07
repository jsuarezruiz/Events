using HolaXamarinFormsMVVM.ViewModels;
using Xamarin.Forms;

namespace HolaXamarinFormsMVVM.Views
{
    public partial class MainView : ContentPage
    {
        public MainView()
        {
            InitializeComponent();

            BindingContext = App.Locator.MainViewModel;
        }
    }
}
