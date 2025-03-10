using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using HaFT.Utilities.Razor.EntityFrameworkCore.Pages;

namespace HaFT.Utilities.Razor.Test.Pages;

using DB;
using Models;
using Column = Models.Table.Column;

public class PersonsModel : TablePageModel<Person>
{
	#region Static
	static PersonsModel()
	{
		var options = new DbContextOptionsBuilder<Database>()
			.UseInMemoryDatabase("DB")
			.Options;

		_db = new Database(options);

		_db.Database.EnsureDeleted();
		_db.Database.EnsureCreated();

		_db.AddRange(p("John", "Doe"), p("Jane", "Doe"));

		_db.AddRange(
			Enumerable.Range(1, 100)
				.Select(_ => p(guid(), guid())));

		_db.SaveChanges();

		static string guid() => Guid.NewGuid().ToString();
		static Person p(string fn, string ln) => new() { FirstName = fn, LastName = ln };
	}

	static readonly Database _db;

	static readonly IReadOnlyList<Column> s_columns =
	[
		Column.Center("#"),
		Column.Left("First Name"),
		Column.Left("Last Name")
	];
	#endregion

	public PersonsModel()
	{
		RowsPerPage = 10;
	}

	[BindProperty]
	public FilterType? Filter { get; set; }

	protected override IQueryable<Person> Query => _db.Persons;
	protected override void ApplySort(ref IQueryable<Person> query) { }

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

	protected override Table GetTable(IQueryable<Person> query, int startingNum)
	{
		return Table.FromList(s_columns, query, toRow!);

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
