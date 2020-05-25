namespace HaFT.Utilities.HtmlTextBuilder
{
	public class Column
	{
		public string Header { get; set; }
		public eHorizontalAlignment Alignment { get; set; }

		#region Constructors
		public Column() { }

		public Column(string header, eHorizontalAlignment alignment)
		{
			Header = header;
			Alignment = alignment;
		}

		public static Column Left(string header) => new Column(header, eHorizontalAlignment.Left);
		public static Column Center(string header) => new Column(header, eHorizontalAlignment.Center);
		#endregion
	}
}
