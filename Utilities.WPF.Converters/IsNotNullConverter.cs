using System;
using System.Windows.Data;
using System.Globalization;

namespace HaFT.Utilities.WPF.Converters;

[ValueConversion(typeof(object), typeof(bool))]
public class IsNotNullConverter : IValueConverter
{
	public object Convert(object? value, Type targetType, object parameter, CultureInfo culture)
	{
		return value != null;
	}

	public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
	{
		throw new NotSupportedException();
	}
}
