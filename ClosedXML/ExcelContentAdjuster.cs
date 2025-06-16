using ClosedXML.Excel;

namespace HaFT.Utilities.ClosedXML;

public class ExcelContentAdjuster
{
	public int FirstRow { get; set; } = 1;
	public required int LastRow { get; set; }
	
	public int FirstColumn { get; set; } = 1;
	public required int LastColumn { get; set; }

	public string? TableName { get; set; }

	public double RowHeight { get; set; } = 20;
	public double ExtraColumnWidth { get; set; } = 5;

	public XLBorderStyleValues? Border { get; set; } = XLBorderStyleValues.Medium;

	public XLAlignmentVerticalValues? VerticalAlignment { get; set; } = XLAlignmentVerticalValues.Center;
	public XLAlignmentHorizontalValues? HorizontalAlignment { get; set; } = XLAlignmentHorizontalValues.Center;

	public void Adjust(IXLWorksheet sheet)
	{
		sheet.Rows(FirstRow, LastRow).Height = RowHeight;

		for (int i = FirstColumn; i <= LastColumn; i++)
		{
			var column = sheet.Column(i);

			column.AdjustToContents();
			column.Width += ExtraColumnWidth;
		}

		var range = sheet.Range(FirstRow, FirstColumn, LastRow, LastColumn);

		var style = range.Style;
		if (Border.HasValue)
			style.Border.SetOutsideBorder(Border.Value);

		var alignment = style.Alignment;
		if (VerticalAlignment.HasValue) alignment.Vertical = VerticalAlignment.Value;
		if (HorizontalAlignment.HasValue) alignment.Horizontal = HorizontalAlignment.Value;

		if (TableName != null)
			range.CreateTable(TableName);
	}
}
