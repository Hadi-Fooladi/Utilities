using System.Linq.Expressions;

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
		Cols.FirstName,
		Cols.LastName,
		Cols.Num1,
		Cols.Num2,
		Cols.Num3
	];
	#endregion

	public PersonsModel()
	{
		RowsPerPage = 10;
		Columns = s_columns;
		ShowStatistics =
		ShowFilters = false;
	}

	[BindProperty]
	public FilterType? Filter { get; set; }

	protected override IQueryable<Person> Query => _db.Persons;

	protected override void ApplySort(ref IQueryable<Person> query)
	{
		foreach (var rule in SortRules)
		{
			Expression<Func<Person, object?>>? expr = null;

			if (rule.Column == Cols.FirstName) expr = p => p.FirstName;
			else
				if (rule.Column == Cols.LastName) expr = p => p.LastName;
				else
					if (rule.Column == Cols.Num1) expr = p => p.Num1;
					else
						if (rule.Column == Cols.Num2) expr = p => p.Num2;
						else
							if (rule.Column == Cols.Num3) expr = p => p.Num3;

			if (expr == null) continue;

			if (query is IOrderedQueryable<Person> q)
				query = rule.Direction == SortDirection.Ascending
					? q.ThenBy(expr)
					: q.ThenByDescending(expr);
			else
				query = rule.Direction == SortDirection.Ascending
					? query.OrderBy(expr)
					: query.OrderByDescending(expr);
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
			yield return p.Num1;
			yield return p.Num2;
			yield return p.Num3;
		}
	}

	protected override void CustomizeTableAppearance()
	{
		Table.GetSortNumberHTML = num => num is >= 1 and <= 9 ? $"<i class='bi bi-{num}-circle ms-1'></i>" : $"{num}";
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

	static class Cols
	{
		public static readonly Column
			FirstName = Column.Left("First Name").Sortable(),
			LastName = Column.Left("Last Name").Sortable(),
			Num1 = Column.Center("Num1").Sortable(),
			Num2 = Column.Center("Num2").Sortable(),
			Num3 = Column.Center("Num3").Sortable();
	}
}
