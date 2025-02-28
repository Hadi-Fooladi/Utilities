using System.Linq;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

namespace HaFT.Utilities.Razor.Test;

class QueryBuilder<T>
{
	public QueryBuilder(IQueryable<T> query) { Query = query; }

	public IQueryable<T> Query { get; private set; }
	public LinkedList<string>? Texts { get; private set; }

	public void CheckEquiality(string propertyName, string? value)
	{
		if (string.IsNullOrWhiteSpace(value)) return;

		AddFilterText(propertyName, value);
		Query = Query.Where(p => EF.Property<string>(p, propertyName) == value);
	}

	public void CheckContains(string propertyName, string? value)
	{
		if (string.IsNullOrWhiteSpace(value)) return;

		AddFilterText(propertyName, value);
		Query = Query.Where(p => EF.Property<string>(p, propertyName).Contains(value));
	}

	public void AddFilterText(string variable, object value)
	{
		Texts ??= [];
		Texts.AddLast($"{variable} = {value}");
	}
}
