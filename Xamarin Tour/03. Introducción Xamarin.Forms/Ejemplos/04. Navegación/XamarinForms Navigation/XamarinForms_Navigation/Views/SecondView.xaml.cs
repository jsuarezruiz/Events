using Xamarin.Forms;
using XamarinForms_Navigation.ViewModels;

namespace XamarinForms_Navigation.Views
{
    public partial class SecondView : ContentPage
    {
        public SecondView()
        {
            InitializeComponent();

            BindingContext = new SecondViewModel(this.Navigation); 
        }
    }
}
