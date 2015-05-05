using System;
using System.IO;
using System.IO.IsolatedStorage;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace Recetario.Converters
{
    public class StringImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var file = value.ToString();

            var storage = IsolatedStorageFile.GetUserStoreForApplication();
            if (storage.FileExists(file))
            {
                var bitmap = new BitmapImage {CreateOptions = BitmapCreateOptions.None};
                var stream = storage.OpenFile(file, FileMode.Open);
                bitmap.SetSource(stream);
                stream.Close();

                return bitmap;
            }
            else
            {
                var stream = storage.CreateFile(file);

                return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
