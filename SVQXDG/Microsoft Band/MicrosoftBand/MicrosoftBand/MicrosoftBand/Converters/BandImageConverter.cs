namespace MicrosoftBand.Converters
{
    using Microsoft.Band.Portable;
    using System;
    using System.Globalization;
    using Xamarin.Forms;

    public class BandImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var image = value as BandImage;

            if (image != null)
            {
                return new StreamImageSource
                {
                    Stream = ct => image.ToStreamAsync()
                };
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
