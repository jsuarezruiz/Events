using System;
using System.ComponentModel;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using dotnetspain2015.CustomControls;
using dotnetspain2015.WinPhone.Controls;
using Microsoft.Phone.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.WinPhone;

[assembly: ExportRenderer((typeof(HubTileView)), typeof(HubTileViewRenderer))]
namespace dotnetspain2015.WinPhone.Controls
{
    class HubTileViewRenderer : ViewRenderer<HubTileView, HubTile>
    {
        private HubTile HubTile;

        public HubTileViewRenderer()
        {
            HubTile = new HubTile
            {
                Margin = new System.Windows.Thickness(5)
            };
        }

        protected override void OnElementChanged(ElementChangedEventArgs<HubTileView> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null || Element == null)
                return;

            HubTile.Title = Element.Title;
            HubTile.Message = Element.Message;
            var fileImageSource = Element.Source as FileImageSource;
            if(fileImageSource != null)
                HubTile.Source = new BitmapImage(new Uri(fileImageSource.File, UriKind.RelativeOrAbsolute));

            System.Windows.Media.Color color = System.Windows.Media.Color.FromArgb(
                (byte)(Element.Color.A * 255),
                (byte)(Element.Color.R * 255),
                (byte)(Element.Color.G * 255),
                (byte)(Element.Color.B * 255));

            HubTile.Background = new SolidColorBrush(color);

            SetNativeControl(HubTile);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (Control == null || Element == null)
                return;

            if (e.PropertyName == HubTileView.TitleProperty.PropertyName)
                HubTile.Title = Element.Title;

            if (e.PropertyName == HubTileView.MessageProperty.PropertyName)
                HubTile.Message = Element.Message;
        }
    }
}
