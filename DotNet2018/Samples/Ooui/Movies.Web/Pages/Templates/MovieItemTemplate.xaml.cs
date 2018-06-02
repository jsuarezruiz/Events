using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Movies.Web.Pages.Templates
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MovieItemTemplate : ContentView
	{
		public MovieItemTemplate ()
		{
			InitializeComponent ();
		}
	}
}