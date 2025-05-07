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

	[Fact]
	public void Remove()
	{
		var builder = new StyleBuilder()
			.Add("color", "red")
			.Add("font-size", 12)
			.Remove("color");

		Assert.Null(builder["color"]);
		Assert.Equal(12, builder["font-size"]);
		Assert.Equal("font-size:12", builder.ToString());
	}

	[Fact]
	public void Base()
	{
		var builder = new StyleBuilder { Base = s_base }
			.Add("font-size", 14);
		
		Assert.Equal("red", builder["color"]);
		Assert.Equal(14, builder["font-size"]);
		Assert.Equal("color:red;font-size:14", builder.ToString());
	}

	[Fact]
	public void Discard()
	{
		var builder = new StyleBuilder { Base = s_base }
			.Discard("color");

		Assert.Equal(SpecialValue.Discard, builder["color"]);
		Assert.Equal(12, builder["font-size"]);
		Assert.Equal("font-size:12", builder.ToString());
	}

	[Fact]
	public void DiscardThenReintroduce()
	{
		var @base = new StyleBuilder { Base = s_base }
			.Discard("color");

		Assert.Equal(12, @base["font-size"]);

		var builder = new StyleBuilder { Base = @base }
			.Discard("font-size")
			.Add("color", "navy");

		Assert.Equal("navy", builder["color"]);
		Assert.Equal(SpecialValue.Discard, builder["font-size"]);

		Assert.Equal("color:navy", builder.ToString());
	}

	static readonly StyleBuilder s_base = new StyleBuilder()
		.Add("color", "red")
		.Add("font-size", 12);
}
