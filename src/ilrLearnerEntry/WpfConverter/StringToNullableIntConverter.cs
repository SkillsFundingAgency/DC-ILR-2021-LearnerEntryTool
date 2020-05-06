using System;
using System.Windows.Data;
using System.Globalization;

namespace ilrLearnerEntry.WpfConverter
{
	public class StringToNullableIntConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			String sReturn = string.Empty;
			if (value != null)
			{
                bool result = Int32.TryParse(System.Convert.ToString(value), out var number);
				if (result)
				{
					sReturn = number.ToString(culture);
				}
			}
			return sReturn;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			string s = (string)value;
			if (String.IsNullOrEmpty(s))
				return null;
			else
			{
				try
				{
					long? X = (long?)long.Parse(s, culture);
					if (X > 2147483647)
						return (int?)2147483647;
					else
						return (int?)int.Parse(s, culture);
				}
				catch
				{
					return null;
				}
			}
		}
	}
}

