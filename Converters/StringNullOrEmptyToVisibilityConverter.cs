using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace StudentManagement
{
    public class StringNullOrEmptyToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Return Visible if the string is not null or empty, otherwise Hidden
            return string.IsNullOrEmpty(value as string) ? Visibility.Collapsed : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // The ConvertBack method is not used in this case
            throw new NotImplementedException();
        }
    }
}
