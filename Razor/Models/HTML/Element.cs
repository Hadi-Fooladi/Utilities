using System.Linq;
using System.Collections.Generic;

namespace HaFT.Utilities.Razor.Models.HTML;

public class Element
{
	public required string Tag { get; set; }

	public string? Classes
	{
		get => GetAttribute("class");
		set => SetAttribute("class", value);
	}

	public Dictionary<string, string> Attributes { get; set; } = [];

	public IReadOnlyCollection<object>? Children { get; set; }

	public override string ToString()
	{
		var attributes = string.Concat(Attributes.Select(p => $" {p.Key}='{p.Value}'"));
		return $"<{Tag}{attributes}>{string.Concat(Children ?? [])}</{Tag}>";
	}

	protected string? GetAttribute(string name) => Attributes.GetValueOrDefault(name);

	/// <param name="value">If `null`, removes the attribute</param>
	protected void SetAttribute(string name, string? value)
	{
		if (value == null) Attributes.Remove(name);
		else Attributes[name] = value;
	}
}
