namespace HaFT.Utilities.Razor.Tests;

using Models.HTML;

public class StyleBuilderTests
{
	[Fact]
	public void Empty()
	{
		var builder = new StyleBuilder();

		Assert.Empty(builder.Names);
		Assert.Equal(string.Empty, builder.ToString());
	}
}
