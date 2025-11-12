using System;
using System.Linq;
using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;

namespace HaFT.Utilities.Razor.EntityFrameworkCore.Pages;

using Models;
using Models.HTML;

public class AutoAppendTablePageModel : BaseTablePageModel
{
	[BindProperty(SupportsGet = true), HiddenInput]
	public int OriginalSortByColumnIndex { get; set; }

	[BindProperty(SupportsGet = true), HiddenInput]
	public SortDirection OriginalSortDirection { get; set; }

	[BindProperty(SupportsGet = true), HiddenInput]
	public string? Filters { get; set; }
}

public abstract class AutoAppendTablePageModel<TEntity> : AutoAppendTablePageModel
{
	public int RowsPerPage { get; set; } = 100;

	protected abstract IQueryable<TEntity> Query { get; }
	protected abstract void ApplySort(ref IQueryable<TEntity> query);
	protected abstract void ApplyFilters(ref IQueryable<TEntity> query, out IEnumerable<string>? filterTexts);
	protected abstract RowCollection GetRows(IQueryable<TEntity> query, int startingNum);

	/// <summary>
	/// Used to store filters
	/// </summary>
	protected abstract string? SerializeFilters();

	/// <summary>
	/// Used to restore filters
	/// </summary>
	protected abstract void DeserializeFilters(string? value);

	void Run() { Run(Query); }
	void Run(IQueryable<TEntity> query)
	{
		const string JS_OBJECT = "HaFT.Razor.EntityFrameworkCore.TableWithPagination";

		ApplyFilters(ref query, out var filterTexts);
		if (filterTexts != null) FilterText = $"Filters: {string.Join(", ", filterTexts)}";

		int
			count = query.Count(),
			pageCount = (int)Math.Ceiling(count / (double)RowsPerPage);

		Statistics = $"{count} row{(count == 1 ? "" : "s")}";

		ApplySort(ref query);

		query = query.Take(RowsPerPage);

		var table = new Table(Columns, GetRows(query, 1))
		{
			SortDirection = SortDirection,
			SortedColumn = SortByColumnIndex >= 0 ? Columns[SortByColumnIndex] : null,
			SortUpCallBack = $"{JS_OBJECT}.sortUp",
			SortDownCallBack = $"{JS_OBJECT}.sortDown",
			ClearSortCallBack = $"{JS_OBJECT}.clearSort",
			AfterRowsHtml = pageCount > 1 ? GetLoadMoreRowHtml(2) : null
		};
		Table = table;

		ModelState.Remove(nameof(Filters));
		ModelState.Remove(nameof(OriginalSortDirection));
		ModelState.Remove(nameof(OriginalSortByColumnIndex));

		Filters = SerializeFilters();
		OriginalSortDirection = SortDirection;
		OriginalSortByColumnIndex = SortByColumnIndex;
	}

	public virtual void OnGet() { Run(); }

	public virtual void OnPost() { Run(); }

	public virtual IActionResult OnGetLoadMore([FromQuery] int page, [FromQuery] int rowsPerPage)
	{
		DeserializeFilters(Filters);

		var query = Query;
		ApplyFilters(ref query, out _);

		int
			count = query.Count(),
			pageCount = (int)Math.Ceiling(count / (double)rowsPerPage),
			skipCount = (page - 1) * rowsPerPage;

		ApplySort(ref query);

		query = query
			.Skip(skipCount)
			.Take(rowsPerPage);

		var table = new Table(Columns, GetRows(query, skipCount + 1))
		{
			AfterRowsHtml = page < pageCount ? GetLoadMoreRowHtml(page + 1, rowsPerPage) : null
		};

		return Partial("TableRows", table);
	}

	string GetLoadMoreRowHtml(int page) => GetLoadMoreRowHtml(page, RowsPerPage);
	string GetLoadMoreRowHtml(int page, int rowsPerPage)
	{
		var uid = $"tr_{Guid.NewGuid():N}";
		var element = new Element
		{
			Tag = "button",
			Children = ["Load more..."],
			Attributes = new()
			{
				["class"] = "btn btn-primary btn-light",
				["hx-get"] = $"?handler=LoadMore&page={page}&rowsPerPage={rowsPerPage}",
				["hx-swap"] = "outerHTML",
				["hx-target"] = $"#{uid}",
				["hx-include"] = FormId == null ? "closest form" : $"#{FormId}"
			}
		};

		return $"<tr id='{uid}'><td class='text-center' colspan='{Columns.Count}'>{element}</td></tr>";
	}
}
