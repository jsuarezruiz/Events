using Xamarin.Forms;

namespace dotnetspain2015.CustomControls
{
    public class HyperLinkControl : Label
    {
        public static readonly BindableProperty NavigateUriProperty;

        static HyperLinkControl()
        {
            NavigateUriProperty = BindableProperty.Create("NavigateUri", typeof (string), typeof (HyperLinkControl),
                string.Empty);
        }

        public string NavigateUri
        {
            get { return (string) base.GetValue(NavigateUriProperty); }
            set { base.SetValue(NavigateUriProperty, value); }
        }
    }
}