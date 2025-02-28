using Microsoft.AspNetCore.Razor.TagHelpers;

namespace HaFT.Utilities.Razor.TagHelpers;

[HtmlTargetElement("input", Attributes = "filter-name", TagStructure = TagStructure.WithoutEndTag)]
[HtmlTargetElement("select", Attributes = "filter-name", TagStructure = TagStructure.NormalOrSelfClosing)]
public class FilterTagHelper : TagHelper
{
	public string FilterName { get; set; } = null!;

	public string FilterColClass { get; set; } = "col";

	public override void Process(TagHelperContext context, TagHelperOutput output)
	{
		var cls = output.TagName == "input" ? "form-control" : "form-select";

		var attributes = output.Attributes;

		attributes.RemoveAll("filter-*");
		attributes.SetAttribute("class", cls);

		output.PreElement.SetHtmlContent(
$"""
<div class='{FilterColClass}'>
	<div class='input-group'>
		<span class='input-group-text align-middle'><pre style='margin:0'>{FilterName}:</pre></span>
""");

		output.PostElement.SetHtmlContent("</div></div>");
	}
}
