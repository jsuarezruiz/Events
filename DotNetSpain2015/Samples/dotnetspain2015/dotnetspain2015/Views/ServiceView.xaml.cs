using dotnetspain2015.ViewModels;
using Xamarin.Forms;

namespace dotnetspain2015.Views
{
    public partial class ServiceView : ContentPage
    {
        public ServiceView()
        {
            InitializeComponent();

            BindingContext = new ServicesViewModel();
        }
    }
}
