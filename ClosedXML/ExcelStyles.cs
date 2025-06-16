using ClosedXML.Excel;

namespace HaFT.Utilities.ClosedXML;

using static StyleModifiers;

public static class ExcelStyles
{
	public static void Normal(IXLStyle style) { AddThinBorder(style); }

	public static void Header(IXLStyle style)
	{
		style.Font.Bold = true;
		AddMediumBorder(style);
		Fill(style, XLColor.Silver);
	}
}
