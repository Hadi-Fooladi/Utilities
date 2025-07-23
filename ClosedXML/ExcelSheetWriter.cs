using System;

using ClosedXML.Excel;

namespace HaFT.Utilities.ClosedXML;

public class ExcelSheetWriter
{
	readonly IXLWorksheet _sheet;

	public ExcelSheetWriter(IXLWorksheet sheet) { _sheet = sheet; }

	public int Row { get; set; } = 1;
	public int Col { get; set; } = 1;

	public bool IncreaseColumnAfterWrite { get; set; } = true;

	public Action<IXLStyle>? StyleChanger { get; set; }

	public IXLCell CurrentCell => _sheet.Cell(Row, Col);

	public void MoveTo(int row, int col) { Row = row; Col = col; }

	public void Write(object? value) { Write(value, StyleChanger); }

	public void Write(object? value, Action<IXLStyle>? styleChanger)
	{
		var cell = CurrentCell;
		cell.Value = XLCellValue.FromObject(value);

		styleChanger?.Invoke(cell.Style);

		if (IncreaseColumnAfterWrite) Col++;
	}

	public void NewLine()
	{
		Row++;
		Col = 1;
	}

	public void SetRowHeight(double height) { _sheet.Row(Row).Height = height; }

	public void AdjustRowHeight(double? add = null)
	{
		var row = _sheet.Row(Row);
		row.AdjustToContents();
		if (add != null)
			row.Height += add.Value;
	}
}
