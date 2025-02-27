using System;
using System.Linq;
using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HaFT.Utilities.Razor.EntityFrameworkCore.Pages;

using Models;

public class TablePageModel : PageModel
{
	public Table Table { get; protected set; } = null!;

	public string SortText { get; protected set; } = "Not sorted";
	public string FilterText { get; protected set; } = "No filters";
	public string Statistics { get; protected set; } = null!;

	public bool CollapseFilters { get; protected set; }

	public bool IsPrevEnabled { get; protected set; }
	public bool IsNextEnabled { get; protected set; }

	[TempData(Key = "F1692046-9ED5-4160-96F7-A349EA1A8AB0")]
	public int PageNumber { get; set; } = 1;

	[BindProperty]
	public string? SortBy { get; set; }

	public string? FormId { get; set; }
}

public abstract class TablePageModel<TEntity> : TablePageModel
{
	public int RowsPerPage { get; set; } = 100;

	protected abstract IQueryable<TEntity> Query { get; }
	protected abstract void ApplySort(ref IQueryable<TEntity> query);
	protected abstract void ApplyFilters(ref IQueryable<TEntity> query, out IEnumerable<string>? filterTexts);
	protected abstract Table GetTable(IQueryable<TEntity> query, int startingNum);

	void Run() { Run(Query); }
	void Run(IQueryable<TEntity> query)
	{
		ApplyFilters(ref query, out var filterTexts);
		if (filterTexts != null) FilterText = $"Filters: {string.Join(", ", filterTexts)}";

		int
			count = query.Count(),
			pageCount = (int)Math.Ceiling(count / (double)RowsPerPage),
			skipCount = (PageNumber - 1) * RowsPerPage;

		Statistics = $"{count} row{(count == 1 ? "" : "s")} - Page {PageNumber} of {pageCount}";

		ApplySort(ref query);
		if (!string.IsNullOrWhiteSpace(SortBy)) SortText = $"Sorted by {SortBy}";

		query = query
			.Skip(skipCount)
			.Take(RowsPerPage);

		Table = GetTable(query, skipCount + 1);

		IsPrevEnabled = PageNumber > 1;
		IsNextEnabled = PageNumber < pageCount;
	}

	public void OnGet()
	{
		PageNumber = 1;
		Run();
		CollapseFilters = true;
	}

	public void OnPost()
	{
		PageNumber = 1;
		Run();
	}

	public void OnPostPrev()
	{
		PageNumber = Math.Max(1, PageNumber - 1);
		Run();
		CollapseFilters = true;
	}

	public void OnPostNext()
	{
		PageNumber++;
		Run();
		CollapseFilters = true;
	}
}
