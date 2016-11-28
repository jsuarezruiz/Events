using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Data;

namespace Recetario.Converters
{
    public class UserImagesDisplayConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var list = value as ObservableCollection<string>;
            if (null != list)
                return list.Count == 0 ? Visibility.Visible : Visibility.Collapsed;
            return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
