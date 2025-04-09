using System;
using System.Linq;
using System.Windows.Data;
using System.Globalization;
using System.Collections.Generic;

namespace HaFT.Utilities.WPF.Converters;

public class ConverterChain : List<IValueConverter>, IValueConverter
{
	public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
		=> this.Aggregate(value, (current, converter) => converter.Convert(current, targetType, parameter, culture));

	public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
	{
		throw new NotSupportedException();
	}
}
