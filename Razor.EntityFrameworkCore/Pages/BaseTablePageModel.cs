using System;
using System.Linq;
using System.Text.Json;
using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HaFT.Utilities.Razor.EntityFrameworkCore.Pages;

using Models;

public class BaseTablePageModel : PageModel
{
	/// <summary>
	/// Must be set before the page is rendered.
	/// </summary>
	public IReadOnlyList<Table.Column> Columns { get; set; } = null!;

	public Table Table { get; protected set; } = null!;

	public string FilterText { get; protected set; } = "No filters";
	public string Statistics { get; protected set; } = null!;

	public string? FormId { get; set; }

	/// <summary>
	/// <b>INTERNAL USE ONLY</b>
	/// </summary>
	[BindProperty, HiddenInput]
	public string SortRulesJson { get; set; } = null!;

	public List<SortRule> SortRules { get; set; } = [];

	protected void UpdateSortRulesJson()
	{
		SortRulesJson = JsonSerializer.Serialize(SortRules.Select(convert), s_serializerOptions);

		SortRuleWithIndex convert(SortRule rule)
			=> new()
			{
				IsAscending = rule.Direction == SortDirection.Ascending,
				ColumnIndex = Columns.FindIndex(column => column == rule.Column)
			};
	}

	protected void UpdateSortRules()
	{
		SortRules = JsonSerializer.Deserialize<List<SortRuleWithIndex>>(SortRulesJson, s_serializerOptions)!
				.Select(convert)
				.ToList();

		SortRule convert(SortRuleWithIndex rule)
			=> new()
			{
				Column = Columns[rule.ColumnIndex],
				Direction = rule.IsAscending ? SortDirection.Ascending : SortDirection.Descending
			};
	}

	static JsonSerializerOptions s_serializerOptions = new() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

	class SortRuleWithIndex
	{
		public int ColumnIndex { get; set; }
		public bool IsAscending { get; set; }
	}
}
