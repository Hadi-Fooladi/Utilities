using System;
using System.Windows;
using System.Windows.Data;
using System.Globalization;

using static System.Windows.Visibility;

namespace HaFT.Utilities.WPF.Converters;

[ValueConversion(typeof(bool), typeof(Visibility))]
public class BoolToVisibilityConverter : IValueConverter
{
	public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
	{
		if (value is bool b)
			return b ? Visible : Collapsed;

		throw new ArgumentException();
	}

	public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
	{
		if (value is Visibility v)
			return v == Visible;

		throw new ArgumentException();
	}
}
