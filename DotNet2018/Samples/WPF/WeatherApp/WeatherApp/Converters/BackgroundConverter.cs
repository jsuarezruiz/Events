using System;
using System.Globalization;
using Xamarin.Forms;

namespace WeatherApp.Converters
{
    public class BackgroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return string.Empty;

            var condition = value.ToString();

            if (condition.Contains("cloud"))
            {
                if (Device.RuntimePlatform == Device.WPF)
                    return "Images/clouds-background.jpg";
                else if (Device.RuntimePlatform == Device.UWP)
                    return "Assets/clouds-background.jpg";
                else
                    return "clouds-background.jpg";
            }
            else if (condition.Contains("rain"))
            {
                if (Device.RuntimePlatform == Device.WPF)
                    return "Images/rain-background.jpg";
                else if (Device.RuntimePlatform == Device.UWP)
                    return "Assets/rain-background.jpg";
                else
                    return "rain-background.jpg";
            }
            else if (condition.Contains("sun") || (condition.Contains("clear sky")))
            {
                if (Device.RuntimePlatform == Device.WPF)
                    return "Images/sun-background.jpg";
                else if (Device.RuntimePlatform == Device.UWP)
                    return "Assets/sun-background.jpg";
                else
                    return "sun-background.jpg";
            }
            else
                return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}