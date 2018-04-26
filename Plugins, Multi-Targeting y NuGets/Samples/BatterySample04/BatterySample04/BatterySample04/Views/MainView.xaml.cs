using BatterySample04.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BatterySample04.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MainView : ContentPage
	{
		public MainView ()
		{
			InitializeComponent ();

            BindingContext = new MainViewModel();
        }
	}
}