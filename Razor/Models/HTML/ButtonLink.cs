using System;
using System.Diagnostics.CodeAnalysis;

namespace HaFT.Utilities.Razor.Models.HTML;

public class ButtonLink : Element
{
	[SetsRequiredMembers]
	public ButtonLink() { Tag = "a"; }

	[SetsRequiredMembers]
	public ButtonLink(string href, string text, string classes) : this()
	{
		Href = href;
		Text = text;
		Attributes.Classes = classes;
	}

	public string? Href
	{
		get => Attributes["href"];
		set => Attributes["href"] = value;
	}

	public string Text { set => Children = [value]; }

	public string? Target
	{
		get => Attributes["target"];
		set => Attributes["target"] = value;
	}

	public static ButtonLink Info(string href, string text) => new(href, text, "btn btn-outline-info");
	public static ButtonLink Danger(string href, string text) => new(href, text, "btn btn-outline-danger");
	public static ButtonLink Primary(string href, string text) => new(href, text, "btn btn-outline-primary");

	public ButtonLink OpenInNewTab()
	{
		Target = Guid.NewGuid().ToString();
		return this;
	}
}
