using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml;

namespace XYFocus.Controls
{
    public sealed partial class MyUserControl : UserControl
    {
        public MyUserControl()
        {
            this.InitializeComponent();
        }

        private void OnFocusEngaged(Control sender, FocusEngagedEventArgs e)
        {
            InitialButton.Focus(FocusState.Programmatic);
        }
    }
}
