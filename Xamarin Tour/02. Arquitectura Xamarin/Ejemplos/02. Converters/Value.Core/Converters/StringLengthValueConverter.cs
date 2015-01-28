using System;
using System.Globalization;
using Cirrious.CrossCore.Converters;

namespace Value.Core.Converters
{
    public class StringLengthValueConverter : MvxValueConverter<string, int>
    {
        protected override int Convert(string value, Type targetType, object parameter, CultureInfo culture)
        {
            value = value ?? string.Empty;
            return value.Length;
        }
    }
}
