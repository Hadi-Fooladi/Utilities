using System.Diagnostics.CodeAnalysis;

namespace HaFT.Utilities.Razor.Models.HTML;

public class Form : Element
{
	[SetsRequiredMembers]
	public Form()
	{
		Tag = "form";
		Method = "post";
	}

	public string? Action
	{
		get => Attributes["action"];
		set => Attributes["action"] = value;
	}

	public string? Method
	{
		get => Attributes["method"];
		set => Attributes["method"] = value;
	}

	public string? OnSubmit
	{
		get => Attributes["onsubmit"];
		set => Attributes["onsubmit"] = value;
	}
}
