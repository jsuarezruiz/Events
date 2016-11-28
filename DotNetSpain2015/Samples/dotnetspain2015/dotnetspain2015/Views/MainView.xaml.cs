using dotnetspain2015.ViewModels;
using Xamarin.Forms;

namespace dotnetspain2015.Views
{
    public partial class MainView : ContentPage
    {
        public MainView()
        {
            InitializeComponent();

            BindingContext = new MainViewModel(this.Navigation);
        }
    }
}
