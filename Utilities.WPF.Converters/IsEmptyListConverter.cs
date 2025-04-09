using System;
using System.Collections;
using System.Windows.Data;
using System.Globalization;

namespace HaFT.Utilities.WPF.Converters;

[ValueConversion(typeof(IList), typeof(bool))]
public class IsEmptyListConverter : IValueConverter
{
	public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
		=> value switch
		{
			null => true,
			IList list => list.Count == 0,
			_ => throw new InvalidOperationException()
		};

	public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
	{
		throw new NotSupportedException();
	}
}
