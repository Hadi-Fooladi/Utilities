﻿using System;
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
		Classes = classes;
	}

	public string? Href
	{
		get => GetAttribute("href");
		set => SetAttribute("href", value);
	}

	public string Text { set => Children = [value]; }

	public string? Target
	{
		get => GetAttribute("target");
		set => SetAttribute("target", value);
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
