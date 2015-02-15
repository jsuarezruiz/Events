using Xamarin.Forms;

namespace dotnetspain2015.CustomControls
{
    public class HubTileView : View
    {
        public static readonly BindableProperty TitleProperty = BindableProperty.Create<HubTileView, string>(p => p.Title, string.Empty);

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly BindableProperty MessageProperty = BindableProperty.Create<HubTileView, string>(p => p.Message, string.Empty);

        public string Message
        {
            get { return (string)GetValue(MessageProperty); }
            set { SetValue(MessageProperty, value); }
        }

        public static readonly BindableProperty SourceProperty = BindableProperty.Create<HubTileView, ImageSource>(p => p.Source, string.Empty);

        public ImageSource Source
        {
            get { return (ImageSource)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

        public static readonly BindableProperty ColorProperty = BindableProperty.Create<HubTileView, Color>(p => p.Color, Color.Default);

        public Color Color
        {
            get { return (Color)GetValue(ColorProperty); }
            set { SetValue(ColorProperty, value); }
        }
    }
}
