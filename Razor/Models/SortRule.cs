namespace HaFT.Utilities.Razor.Models;

public class SortRule
{
	public required Table.Column Column { get; set; }
	public SortDirection Direction { get; set; }
}
