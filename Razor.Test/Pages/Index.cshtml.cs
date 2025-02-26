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
		Table = Table.FromList(s_columns, Enumerable.Range(1, 3), toRow);

		static IEnumerable<object> toRow(int num)
		{
			yield return num;
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

	private static readonly IReadOnlyList<Column> s_columns =
	[
		Column.Center("#"),
		Column.Center("ID"),
		Column.Left("First Name"),
		Column.Right("Actions (HStack)"),
		Column.Right("Actions (Array)")
	];
}
