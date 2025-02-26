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
		get => GetAttribute("action");
		set => SetAttribute("action", value);
	}

	public string? Method
	{
		get => GetAttribute("method");
		set => SetAttribute("method", value);
	}

	public string? OnSubmit
	{
		get => GetAttribute("onsubmit");
		set => SetAttribute("onsubmit", value);
	}
}
