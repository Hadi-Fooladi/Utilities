using System;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace HaFT.Utilities.Razor.TagHelpers;

public class CollapsibleTagHelper : TagHelper
{
	public string? Title { get; set; }
	public bool IsCollapsed { get; set; }

	public override void Process(TagHelperContext context, TagHelperOutput output)
	{
		output.TagName = "div";
		output.Attributes.SetAttribute("class", "card mb-2 row");

		var id = $"Collapsible{Guid.NewGuid():N}";

		output.PreContent.SetHtmlContent(
$"""
<div class="card-header">
	<div class="row">
		<div class="col d-flex align-items-center h6 m-0">{Title}</div>
		<div class="col text-end">
			<button class="btn btn-primary" type="button" data-bs-toggle="collapse" data-bs-target="#{id}">▼</button>
		</div>
	</div>
</div>

<div class="card-body collapse{(IsCollapsed ? "" : " show")}" id="{id}">
""");

		output.PostContent.SetHtmlContent("</div>");
	}
}
