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
	public string Statistics { get; protected set; } = "";

	public string? FormId { get; set; }

	/// <summary>
	/// <b>INTERNAL USE ONLY</b>
	/// </summary>
	[BindProperty, HiddenInput]
	public string SortRulesJson { get; set; } = null!;

	public List<SortRule> SortRules { get; set; } = [];

	public bool ShowFilters { get; set; } = true;
	public bool ShowStatistics { get; set; } = true;

	public string StatisticsFiltersText
	{
		get
		{
			return string.Join(" - ", parts());

			IEnumerable<string> parts()
			{
				if (ShowStatistics) yield return Statistics;
				if (ShowFilters) yield return FilterText;
			}
		}
	}

	/// <summary>
	/// Override this method to customize the appearance of the table.<br />
	/// It will be called after the table is instantiated.<br />
	/// Do not call this method directly.<br />
	/// Default implementation does nothing.
	/// </summary>
	protected virtual void CustomizeTableAppearance() { }

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
