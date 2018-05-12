using HotDogOrNot.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HotDogOrNot.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HotDotView : ContentPage
	{
		public HotDotView ()
		{
			InitializeComponent ();

            BindingContext = new HotDogViewModel();
		}
	}
}