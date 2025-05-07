using System;
using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HaFT.Utilities.Razor.EntityFrameworkCore.Pages;

using Models;

public class BaseTablePageModel : PageModel
{
	public required IReadOnlyList<Table.Column> Columns { get; init; }

	public Table Table { get; protected set; } = null!;

	public string FilterText { get; protected set; } = "No filters";
	public string Statistics { get; protected set; } = null!;

	[Obsolete]
	public bool CollapseFilters => false;

	[BindProperty(SupportsGet = true), HiddenInput]
	public int SortByColumnIndex { get; set; } = -1;

	[BindProperty(SupportsGet = true), HiddenInput]
	public SortDirection SortDirection { get; set; }

	public string? FormId { get; set; }
}
