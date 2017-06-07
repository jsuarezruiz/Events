using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinTizenApp.ViewModels;

namespace XamarinTizenApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MonkeysView : ContentPage
    {
        public MonkeysView()
        {
            InitializeComponent();

            BindingContext = new MonkeysViewModel();
        }
    }
}