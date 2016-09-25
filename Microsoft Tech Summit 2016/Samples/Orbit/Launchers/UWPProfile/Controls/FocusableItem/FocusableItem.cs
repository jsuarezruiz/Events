using Windows.UI.Xaml.Controls;

namespace Orbit.Controls
{
    public sealed class FocusableItem : ContentControl
    {
        public FocusableItem()
        {
            this.DefaultStyleKey = typeof(FocusableItem);
            this.IsTabStop = true;
        }
    }
}
