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

		Table.SortedColumn = Table.Columns[2];
		Table.SortDirection = SortDirection.Descending;

		IEnumerable<Row> rows()
		{
			foreach (var num in Enumerable.Range(1, 20))
				yield return new Row(num);
		}
	}

	static readonly IReadOnlyList<Column> s_columns =
	[
		Column.Center("#"),
		Column.Center("<i>First Name</i>").Sortable(),
		Column.Right("Actions (HStack)").Sortable(),
		Column.Right("Actions (Array)").Sortable()
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

			var element = new Element
				{
					Tag = "span",
					Children = ["John Doe"],
					Attributes = { Base = L3 }
				}
				.WithAttribute("ghi", "4");
			yield return element;

			object[] actions =
			[
				ButtonLink.Primary("https://www.google.com", "Google"),
				ButtonLink.Danger("https://www.youtube.com", "Youtube"),
				ButtonLink.Info("https://www.wikipedia.org", "Wikipedia"),
			];

			yield return new HStack { Children = actions };
			yield return actions;
		}

		static readonly AttributeCollection
			ROOT = new AttributeCollection().With("abc", "Root"),
			L2 = new AttributeCollection { Base = ROOT }.With("Abc", "L2"),
			L3 = new AttributeCollection { Base = L2 }.With("def", "L3");
	}
}
