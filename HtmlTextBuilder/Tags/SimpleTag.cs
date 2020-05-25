using System.Text;

namespace HaFT.Utilities.HtmlTextBuilder
{
	/// <summary>
	/// Tag with textual content/attributes
	/// </summary>
	public class SimpleTag : ITag
	{
		public string Name { get; set; }

		public object Content { get; set; }

		public string Attributes { get; set; }

		#region Constructors
		public SimpleTag() { }

		/// <summary>
		/// Empty Tag
		/// </summary>
		public SimpleTag(string name) => Name = name;

		public SimpleTag(string name, string attributes) : this(name) => Attributes = attributes;
		#endregion

		public void AppendTo(StringBuilder sb) => AppendTo(sb, Content);

		public void AppendTo(StringBuilder sb, object content)
		{
			if (Attributes == null)
				if (content == null)
					sb.Append($"<{Name} />");
				else
					sb.AppendFormat("<{0}>{1}</{0}>", Name, content);
			else
				if (content == null)
					sb.Append($"<{Name} {Attributes} />");
				else
					sb.AppendFormat("<{0} {1}>{2}</{0}>", Name, Attributes, content);
		}
	}
}
