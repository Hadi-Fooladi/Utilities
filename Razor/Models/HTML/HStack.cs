using System.Diagnostics.CodeAnalysis;

namespace HaFT.Utilities.Razor.Models.HTML;

public class HStack : Element
{
	[SetsRequiredMembers]
	public HStack()
	{
		Tag = "div";
		Classes = "hstack gap-1 justify-content-center";
	}
}
