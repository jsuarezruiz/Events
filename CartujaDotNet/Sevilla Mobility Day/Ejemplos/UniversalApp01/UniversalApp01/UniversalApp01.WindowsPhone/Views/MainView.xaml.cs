using UniversalApp01.Views.Base;
using Windows.UI.Xaml.Navigation;

namespace UniversalApp01
{
    /// <summary>
    /// MainView
    /// </summary>
    public sealed partial class MainView : PageBase
    {
        public MainView()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;
        }
    }
}
