using Xamarin.Forms;
using XamarinForms_Navigation.ViewModels;

namespace XamarinForms_Navigation.Views
{
    public partial class FirstView : ContentPage
    {
        public FirstView()
        {
            InitializeComponent();

            BindingContext = new FirstViewModel(this.Navigation); 
        }
    }
}
