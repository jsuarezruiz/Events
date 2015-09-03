namespace MicrosoftBand.Converters
{
    using Microsoft.Band.Portable;
    using System;
    using System.Globalization;
    using Xamarin.Forms;

    public class BandColorToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is BandColor)
            {
                var color = ((BandColor)value);
                return Color.FromRgb(color.R, color.G, color.B);
            }

            return Color.Black;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
