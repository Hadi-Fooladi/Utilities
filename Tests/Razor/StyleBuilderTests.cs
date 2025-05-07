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

	[Fact]
	public void Basic()
	{
		var builder = new StyleBuilder()
			.Add("color", "red")
			.Add("font-size", 12);

		Assert.Equal("red", builder["color"]);
		Assert.Equal(12, builder["font-size"]);
		Assert.Equal("color:red;font-size:12", builder.ToString());
	}
}
