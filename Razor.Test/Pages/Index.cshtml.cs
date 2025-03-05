using System.Collections;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HaFT.Utilities.Razor.Test.Pages;

using Models;
using Models.HTML;
using Column = Models.Table.Column;

public class IndexModel : PageModel
{
	public Table Table { get; set; } = null!;

	public void OnGet()
	{
		Table = new Table(s_columns, rows());

		IEnumerable<Row> rows()
		{
			foreach (var num in Enumerable.Range(1, 20))
				yield return new Row(num);
		}
	}

	static readonly IReadOnlyList<Column> s_columns =
	[
		Column.Center("#"),
		Column.Center("ID"),
		Column.Left("First Name"),
		Column.Right("Actions (HStack)"),
		Column.Right("Actions (Array)")
	];

	class Row : ITableRow, IEnumerable<object>
	{
		readonly int _num;

		public Row(int num)
		{
			_num = num;
			Classes = num % 3 == 0 ? "bg-danger text-white" : null;
		}

		public string? Classes { get; }

		IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }

		public IEnumerator<object> GetEnumerator()
		{
			yield return _num;
			yield return "John";

			object[] actions =
			[
				ButtonLink.Primary("https://www.google.com", "Google"),
				ButtonLink.Danger("https://www.youtube.com", "Youtube"),
				ButtonLink.Info("https://www.wikipedia.org", "Wikipedia"),
			];

			yield return new HStack { Children = actions };
			yield return actions;
		}
	}
}
