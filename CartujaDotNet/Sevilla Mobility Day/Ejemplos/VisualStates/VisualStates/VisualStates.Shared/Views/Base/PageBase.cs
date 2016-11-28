using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace VisualStates.Views
{
    public class PageBase : Page
    {
        protected override void OnNavigatedTo(Windows.UI.Xaml.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            this.SizeChanged += Page_SizeChanged;
        }

        protected override void OnNavigatedFrom(Windows.UI.Xaml.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            this.SizeChanged -= Page_SizeChanged;
        }

        void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (e.NewSize.Width < 520)
                VisualStateManager.GoToState(this, "MinimalOrPhonePortrailLayout", true);
            else if (e.NewSize.Width < e.NewSize.Height)
                VisualStateManager.GoToState(this, "PortraitLayout", true);
            else
                VisualStateManager.GoToState(this, "DefaultLayout", true);
        }
    }
}
