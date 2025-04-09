using System;
using System.Windows.Data;
using System.Globalization;

namespace HaFT.Utilities.WPF.Converters;

[ValueConversion(typeof(bool), typeof(bool))]
public class BoolInverterConverter : IValueConverter
{
	public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
	{
		if (value is bool b) return !b;

		throw new InvalidOperationException();
	}

	public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
		=> Convert(value, targetType, parameter, culture);
}
