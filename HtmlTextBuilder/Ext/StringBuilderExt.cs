using System;
using System.Text;
using System.Collections.Generic;

namespace HaFT.Utilities.HtmlTextBuilder.Ext
{
	public static class StringBuilderExt
	{
		public static void OpenTag(this StringBuilder sb, string tagName) => sb.Append($"<{tagName}>");
		public static void CloseTag(this StringBuilder sb, string tagName) => sb.Append($"</{tagName}>");

		public static void OpenTag(this StringBuilder sb, string tagName, params Attribute[] attributes)
			=> sb.OpenTag(tagName, (IEnumerable<Attribute>)attributes);

		public static void OpenTag(this StringBuilder SB, string tagName, IEnumerable<Attribute> attributes)
		{
			SB.Append($"<{tagName}");
			foreach (var a in attributes)
			{
				SB.Append(" ");
				SB.Append(a);
			}
			SB.Append(">");
		}
	}
}
