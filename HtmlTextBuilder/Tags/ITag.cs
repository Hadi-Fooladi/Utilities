using System.Text;

namespace HaFT.Utilities.HtmlTextBuilder
{
	public interface ITag
	{
		string Name { get; set; }

		void AppendTo(StringBuilder sb);
		void AppendTo(StringBuilder sb, object content);
	}
}
