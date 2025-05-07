using System;
using System.Linq;
using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;

namespace HaFT.Utilities.Razor.EntityFrameworkCore.Pages;

using Models;

public class AutoAppendTablePageModel : BaseTablePageModel
{
	public bool IsPrevEnabled { get; protected set; }
	public bool IsNextEnabled { get; protected set; }

	[TempData(Key = "F1692046-9ED5-4160-96F7-A349EA1A8AB0")]
	public int PageNumber { get; set; } = 1;
}

public abstract class AutoAppendTablePageModel<TEntity> : AutoAppendTablePageModel
{
	public int RowsPerPage { get; set; } = 100;

	protected abstract IQueryable<TEntity> Query { get; }
	protected abstract void ApplySort(ref IQueryable<TEntity> query);
	protected abstract void ApplyFilters(ref IQueryable<TEntity> query, out IEnumerable<string>? filterTexts);
	protected abstract RowCollection GetRows(IQueryable<TEntity> query, int startingNum);

	void Run() { Run(Query); }
	void Run(IQueryable<TEntity> query)
	{
		const string JS_OBJECT = "HaFT.Razor.EntityFrameworkCore.TableWithPagination";

		ApplyFilters(ref query, out var filterTexts);
		if (filterTexts != null) FilterText = $"Filters: {string.Join(", ", filterTexts)}";

		int
			count = query.Count(),
			pageCount = (int)Math.Ceiling(count / (double)RowsPerPage),
			skipCount = (PageNumber - 1) * RowsPerPage;

		Statistics = $"{count} row{(count == 1 ? "" : "s")} - Page {PageNumber} of {pageCount}";

		ApplySort(ref query);

		query = query
			.Skip(skipCount)
			.Take(RowsPerPage);

		var table = new Table(Columns, GetRows(query, skipCount + 1))
		{
			SortDirection = SortDirection,
			SortedColumn = SortByColumnIndex >= 0 ? Columns[SortByColumnIndex] : null,
			SortUpCallBack = $"{JS_OBJECT}.sortUp",
			SortDownCallBack = $"{JS_OBJECT}.sortDown",
			ClearSortCallBack = $"{JS_OBJECT}.clearSort"
		};
		Table = table;

		IsPrevEnabled = PageNumber > 1;
		IsNextEnabled = PageNumber < pageCount;
	}

	public void OnGet()
	{
		PageNumber = 1;
		Run();
	}

	static int _counter;

	public IActionResult OnGetTest()
	{
		return Content($"From Test!!! {_counter++} {SortByColumnIndex}");
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
	}

	public void OnPostNext()
	{
		PageNumber++;
		Run();
	}
}
