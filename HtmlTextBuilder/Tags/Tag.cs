using System;
using System.Text;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace HaFT.Utilities.HtmlTextBuilder
{
	using Ext;

	public class Tag : ITag
	{
		public string Name { get; set; }

		/// <summary>
		/// Expected values:
		///   + null: Empty tag
		///   + object
		///   + IEnumerable containing object and Tag instances
		/// </summary>
		public object Content { get; set; }

		private List<Attribute> Attributes;

		#region Constructors
		public Tag() { }

		/// <summary>
		/// Empty Tag
		/// </summary>
		public Tag(string name) => Name = name;

		public Tag(string name, params Attribute[] attributes) : this(name) => Attributes = attributes.ToList();
		#endregion

		public void AddAttribute(Attribute a)
		{
			if (Attributes == null) Attributes = new List<Attribute>();

			Attributes.Add(a);
		}

		public void AppendTo(StringBuilder sb) => AppendTo(sb, Content);

		public void AppendTo(StringBuilder sb, object content)
		{
			sb.Append($"<{Name}");

			if (Attributes != null)
				sb.Append(string.Join(" ", Attributes));

			if (content == null)
			{
				sb.Append(" />");
				return;
			}

			sb.Append(">");
			AddContent(content);
			sb.CloseTag(Name);

			void AddContent(object value)
			{
				switch (value)
				{
				case Tag T:
					T.AppendTo(sb);
					break;

				case IEnumerable L:
					foreach (var X in L)
						AddContent(X);
					break;

				default:
					sb.Append(value);
					break;
				}
			}
		}
	}
}
