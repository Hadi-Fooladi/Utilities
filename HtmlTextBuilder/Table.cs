using System;
using System.Text;
using System.Collections.Generic;

namespace HaFT.Utilities.HtmlTextBuilder
{
	using Ext;
	using RowCollection = IEnumerable<IEnumerable<object>>;

	public class Table : ITag
	{
		public string Name { get; set; } = "table";

		public IReadOnlyList<Column> Columns { get; set; }

		public RowCollection Rows { get; set; }

		public IReadOnlyList<Attribute> Attributes { get; set; } = TABLE_ATTRIBUTES;
		public IReadOnlyList<Attribute> HeadAttributes { get; set; } = THEAD_ATTRIBUTES;

		public void AppendTo(StringBuilder sb) => AppendTo(sb, Rows);
		public void AppendTo(StringBuilder sb, object content) => AppendTo(sb, (RowCollection)content);

		private void AppendTo(StringBuilder sb, RowCollection rows)
		{
			sb.OpenTag(Name, Attributes);

			// Headers
			sb.OpenTag("thead", HeadAttributes);
			sb.Append("<tr>");
			foreach (var column in Columns) AddHeader(column);
			sb.Append("</tr></thead>");

			// Cells
			sb.Append("<tbody>");
			foreach (var row in rows)
			{
				sb.Append("<tr>");

				int ndxColumn = 0;
				foreach (var obj in row)
					AddCell(obj, Columns[ndxColumn++]);

				sb.Append("</tr>");
			}

			sb.Append("</tbody></table>");

			void AddHeader(Column column)
			{
				var tag = column.Alignment == eHorizontalAlignment.Center ? Tags.TH_CENTER : Tags.TH;
				tag.AppendTo(sb, column.Header);
			}

			void AddCell(object text, Column column)
			{
				var tag = column.Alignment == eHorizontalAlignment.Center ? Tags.TD_CENTER : Tags.TD;
				tag.AppendTo(sb, text);
			}
		}

		private static readonly Attribute
			TABLE_CLASS = Attribute.Class("table table-bordered table-striped table-hover"),
			THEAD_CLASS = Attribute.Class("thead-dark");

		private static readonly Attribute[]
			TABLE_ATTRIBUTES = { TABLE_CLASS },
			THEAD_ATTRIBUTES = { THEAD_CLASS };

		private static class Tags
		{
			private const string CENTER_CLASS = "class='text-center'";

			public static readonly SimpleTag
				TD = new SimpleTag("td"),
				TD_CENTER = new SimpleTag("td", CENTER_CLASS),
				TH = new SimpleTag("th"),
				TH_CENTER = new SimpleTag("th", CENTER_CLASS);
		}
	}
}
