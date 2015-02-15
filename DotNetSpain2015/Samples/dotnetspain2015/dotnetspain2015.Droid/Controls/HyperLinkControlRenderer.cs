using Android.Text.Util;
using dotnetspain2015.CustomControls;
using dotnetspain2015.Droid.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(HyperLinkControl), typeof(HyperLinkControlRenderer))]
namespace dotnetspain2015.Droid.Controls
{
    public class HyperLinkControlRenderer : LabelRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement == null)
            {
                var nativeEditText = Control;

                Linkify.AddLinks(nativeEditText, MatchOptions.All);
            }
        }
    }
}