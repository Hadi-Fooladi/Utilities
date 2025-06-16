using ClosedXML.Excel;

namespace HaFT.Utilities.ClosedXML;

public static class StyleModifiers
{
	public static void AddThinBorder(IXLStyle style) { style.Border.SetOutsideBorder(XLBorderStyleValues.Thin); }
	public static void AddMediumBorder(IXLStyle style) { style.Border.SetOutsideBorder(XLBorderStyleValues.Medium); }

	public static void Fill(IXLStyle style, XLColor color) { style.Fill.BackgroundColor = color; }
}
