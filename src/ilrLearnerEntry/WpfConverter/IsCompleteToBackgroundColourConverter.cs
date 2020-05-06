
using System;
using System.Windows.Data;

namespace ilrLearnerEntry.WpfConverter
{
    public class IsCompleteToBackgroundColourConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return string.Empty;

            return System.Convert.ToBoolean(value) ? "#FFBDE891" : "#FFE89191";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return string.Empty;
        }
    }
}

