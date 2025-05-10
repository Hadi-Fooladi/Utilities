global using RowCollection = System.Collections.Generic.IEnumerable<System.Collections.Generic.IEnumerable<object?>>;

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace HaFT.Utilities.Razor.Models;

public class Table
{
	public Table() { }

	[SetsRequiredMembers]
	public Table(IReadOnlyList<Column> columns, RowCollection rows)
	{
		Rows = rows;
		Columns = columns;
	}

	public static Table FromList<T>(IReadOnlyList<Column> columns, IEnumerable<T> list, Func<T, IEnumerable<object>> toRow)
		=> new(columns, list.GenerateRows(toRow));

	public required RowCollection Rows { get; set; }
	public required IReadOnlyList<Column> Columns { get; set; }

	public Column? SortedColumn { get; set; }
	public SortDirection SortDirection { get; set; }

	public string SortHtml { get; set; } = SORT;
	public string SortUpHtml { get; set; } = SORT_UP;
	public string SortDownHtml { get; set; } = SORT_DOWN;

	public string? SortUpCallBack { get; set; } /*= "(function(ndx) { alert('Sort Up' + ndx) })";*/
	public string? SortDownCallBack { get; set; } /*= "(function(ndx) { alert('Sort Down' + ndx) })";*/
	public string? ClearSortCallBack { get; set; } /*= "(function(ndx) { alert('Clear Sort' + ndx) })";*/

	public string? AfterRowsHtml { get; set; }

	#region Nested Classes
	public class Column
	{
		public string Header { get; set; } = "";
		public HorizontalAlignment Alignment { get; set; }
		public bool IsSortable { get; set; }

		#region Constructors
		public Column() { }

		public Column(string header, HorizontalAlignment alignment)
		{
			Header = header;
			Alignment = alignment;
		}

		public static Column Left(string header) => new(header, HorizontalAlignment.Left);
		public static Column Right(string header) => new(header, HorizontalAlignment.Right);
		public static Column Center(string header) => new(header, HorizontalAlignment.Center);
		#endregion

		public Column Sortable()
		{
			IsSortable = true;
			return this;
		}
	}
	#endregion

	const string
		SORT = "<svg xmlns=\"http://www.w3.org/2000/svg\" viewBox=\"0 0 320 512\"><!--!Font Awesome Free 6.7.2 by @fontawesome - https://fontawesome.com License - https://fontawesome.com/license/free Copyright 2025 Fonticons, Inc.--><path d=\"M137.4 41.4c12.5-12.5 32.8-12.5 45.3 0l128 128c9.2 9.2 11.9 22.9 6.9 34.9s-16.6 19.8-29.6 19.8L32 224c-12.9 0-24.6-7.8-29.6-19.8s-2.2-25.7 6.9-34.9l128-128zm0 429.3l-128-128c-9.2-9.2-11.9-22.9-6.9-34.9s16.6-19.8 29.6-19.8l256 0c12.9 0 24.6 7.8 29.6 19.8s2.2 25.7-6.9 34.9l-128 128c-12.5 12.5-32.8 12.5-45.3 0z\"/></svg>",
		SORT_UP = "<svg xmlns=\"http://www.w3.org/2000/svg\" viewBox=\"0 0 320 512\"><!--!Font Awesome Free 6.7.2 by @fontawesome - https://fontawesome.com License - https://fontawesome.com/license/free Copyright 2025 Fonticons, Inc.--><path d=\"M182.6 41.4c-12.5-12.5-32.8-12.5-45.3 0l-128 128c-9.2 9.2-11.9 22.9-6.9 34.9s16.6 19.8 29.6 19.8l256 0c12.9 0 24.6-7.8 29.6-19.8s2.2-25.7-6.9-34.9l-128-128z\"/></svg>",
		SORT_DOWN = "<svg xmlns=\"http://www.w3.org/2000/svg\" viewBox=\"0 0 320 512\"><!--!Font Awesome Free 6.7.2 by @fontawesome - https://fontawesome.com License - https://fontawesome.com/license/free Copyright 2025 Fonticons, Inc.--><path d=\"M182.6 470.6c-12.5 12.5-32.8 12.5-45.3 0l-128-128c-9.2-9.2-11.9-22.9-6.9-34.9s16.6-19.8 29.6-19.8l256 0c12.9 0 24.6 7.8 29.6 19.8s2.2 25.7-6.9 34.9l-128 128z\"/></svg>";
}

public interface ITableRow
{
	public string? Classes { get; }
}

public static class TableExt
{
	public static RowCollection GenerateRows<T>(
		this IEnumerable<T> list,
		Func<T, IEnumerable<object?>> values,
		int startingNum = 1)
	{
		return rows();

		RowCollection rows()
		{
			int num = startingNum;
			foreach (var item in list)
			{
				yield return row();

				IEnumerable<object?> row()
				{
					yield return num++;
					foreach (var value in values(item))
						yield return value;
				}
			}
		}
	}
}
