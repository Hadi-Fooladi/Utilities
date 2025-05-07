using Microsoft.AspNetCore.Mvc;

using HaFT.Utilities.Razor.EntityFrameworkCore.Pages;

namespace HaFT.Utilities.Razor.Test.Pages;

using DB;
using Models;
using Column = Models.Table.Column;

public class PersonsModel : TablePageModel<Person>
{
	#region Static
	static readonly Database _db = Database.Instance;

	static readonly IReadOnlyList<Column> s_columns =
	[
		Column.Center("#"),
		Column.Left("First Name").Sortable(),
		Column.Left("Last Name").Sortable()
	];
	#endregion

	public PersonsModel()
	{
		RowsPerPage = 10;
		Columns = s_columns;
	}

	[BindProperty]
	public FilterType? Filter { get; set; }

	protected override IQueryable<Person> Query => _db.Persons;

	protected override void ApplySort(ref IQueryable<Person> query)
	{
		switch (SortByColumnIndex)
		{
		case 1:
			query = SortDirection == SortDirection.Ascending
				? query.OrderBy(p => p.FirstName)
				: query.OrderByDescending(p => p.FirstName);
			break;
		case 2:
			query = SortDirection == SortDirection.Ascending
				? query.OrderBy(p => p.LastName)
				: query.OrderByDescending(p => p.LastName);
			break;
		}
	}

	protected override void ApplyFilters(ref IQueryable<Person> query, out IEnumerable<string>? filterTexts)
	{
		if (Filter == null)
		{
			filterTexts = null;
			return;
		}

		query = Filter.Apply(query, out var list);
		filterTexts = list;
	}

	protected override IEnumerable<IEnumerable<object?>> GetRows(IQueryable<Person> query, int startingNum)
	{
		return query.AsEnumerable().GenerateRows(toRow, startingNum);

		static IEnumerable<object?> toRow(Person p)
		{
			yield return p.FirstName;
			yield return p.LastName;
		}
	}

	public class FilterType
	{
		public string? FirstName { get; set; }
		public string? LastName { get; set; }

		public IQueryable<Person> Apply(IQueryable<Person> query, out LinkedList<string>? filterTexts)
		{
			var builder = new QueryBuilder<Person>(query);

			builder.CheckContains("FirstName", FirstName);
			builder.CheckContains("LastName", LastName);

			filterTexts = builder.Texts;
			return builder.Query;
		}
	}
}
