using System;
using System.Globalization;
using Xamarin.Forms;
namespace Embedding.Converters
{
    public class ImageFromManagedResourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string fileName)
            {
                return ImageSource.FromResource($"Embedding.Resources.{fileName}.png", typeof(ImageFromManagedResourceConverter));
            }

            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
