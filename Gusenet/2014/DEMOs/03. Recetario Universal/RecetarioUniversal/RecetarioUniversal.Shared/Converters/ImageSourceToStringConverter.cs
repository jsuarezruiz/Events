using System;
using System.Globalization;
using Windows.UI.Xaml.Data;

namespace RecetarioUniversal.Converters
{
    public class ImageSourceToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            string file = value.ToString();
            if (!file.Contains("/")) return string.Empty;
            int start = file.IndexOf("/", StringComparison.Ordinal) + 1;
            int end = file.IndexOf(".", StringComparison.Ordinal);
            return string.Format("Creado: {0}", file.Substring(start, (end - start)));
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}