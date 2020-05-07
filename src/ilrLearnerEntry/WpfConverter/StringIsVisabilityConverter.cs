
using System;
using System.Windows.Data;

namespace ilrLearnerEntry.WpfConverter
{
    public class StringIsVisabilityConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter,  System.Globalization.CultureInfo culture)
        {
            bool flag = String.IsNullOrEmpty(value.ToString());
            return flag ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is System.Windows.Visibility)
            {
                return (System.Windows.Visibility)value == System.Windows.Visibility.Visible;
            }
            return false;
        }

    }
}

