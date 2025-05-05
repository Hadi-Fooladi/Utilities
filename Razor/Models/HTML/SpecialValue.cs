namespace HaFT.Utilities.Razor.Models.HTML;

public class SpecialValue
{
	/// <summary>
	/// Used to discard the value in the base(s) if it exists.
	/// </summary>
	public static SpecialValue Discard { get; } = new();

	/// <summary>
	/// The attribute will be rendered as 'attribute' instead of 'attribute="value"'
	/// </summary>
	public static SpecialValue Empty { get; set; } = new();
}
