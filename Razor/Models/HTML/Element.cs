using System.Collections.Generic;

namespace HaFT.Utilities.Razor.Models.HTML;

public class Element
{
	public required string Tag { get; set; }

	public AttributeCollection Attributes { get; init; } = new();

	public IReadOnlyCollection<object>? Children { get; set; }

	public override string ToString()
		=> $"<{Tag}{Attributes}>{string.Concat(Children ?? [])}</{Tag}>";
}

public static class ElementExt
{
	public static T WithAttribute<T>(this T element, string name, object? value) where T : Element
	{
		element.Attributes[name] = value;
		return element;
	}
}
