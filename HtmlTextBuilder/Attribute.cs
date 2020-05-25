namespace HaFT.Utilities.HtmlTextBuilder
{
	public class Attribute
	{
		public string Name { get; set; }
		public string Value { get; set; }

		#region Constructors
		public Attribute() { }

		public Attribute(string name, string value)
		{
			Name = name;
			Value = value;
		}

		public static Attribute Class(string value) => new Attribute("class", value);
		#endregion

		public override string ToString() => $"{Name}='{Value}'";
	}
}
