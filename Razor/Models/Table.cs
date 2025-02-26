global using RowCollection = System.Collections.Generic.IEnumerable<System.Collections.Generic.IEnumerable<object>>;

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

	#region Nested Classes
	public class Column
	{
		public string Header { get; set; } = "";
		public HorizontalAlignment Alignment { get; set; }

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
	}
	#endregion
}

public static class TableExt
{
	public static RowCollection GenerateRows<T>(
		this IEnumerable<T> list,
		Func<T, IEnumerable<object>> values,
		int startingNum = 1)
	{
		return rows();

		RowCollection rows()
		{
			int num = startingNum;
			foreach (var item in list)
			{
				yield return row();

				IEnumerable<object> row()
				{
					yield return num++;
					foreach (var value in values(item))
						yield return value;
				}
			}
		}
	}
}
