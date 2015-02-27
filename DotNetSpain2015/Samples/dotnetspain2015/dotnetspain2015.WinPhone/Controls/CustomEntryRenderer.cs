using System.Windows.Media;
using dotnetspain2015.CustomControls;
using dotnetspain2015.WinPhone.Controls;
using Microsoft.Phone.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.WinPhone;

[assembly: ExportRenderer(typeof(CustomEntry), typeof(CustomEntryRenderer))]
namespace dotnetspain2015.WinPhone.Controls
{
    internal class CustomEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement == null)
            {
                var nativePhoneTextBox = (PhoneTextBox)Control.Children[0];
                nativePhoneTextBox.Background = new SolidColorBrush(Colors.Yellow);
            }
        }
    }
}