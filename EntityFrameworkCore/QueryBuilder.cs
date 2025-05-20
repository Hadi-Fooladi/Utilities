using System.Linq;

using Microsoft.EntityFrameworkCore;

namespace HaFT.Utilities.EntityFrameworkCore;

public class QueryBuilder<TEntity>
{
	public QueryBuilder(IQueryable<TEntity> query) { Query = query; }

	public IQueryable<TEntity> Query { get; private set; }

	public QueryBuilder<TEntity> CheckEquiality(string propertyName, object? value)
	{
		Query = Query.Where(p => Equals(EF.Property<object>(p!, propertyName), value));
		return this;
	}

	public QueryBuilder<TEntity> CheckContains(string propertyName, string? value)
	{
		if (!string.IsNullOrEmpty(value))
			Query = Query.Where(p => EF.Property<string>(p!, propertyName).Contains(value));

		return this;
	}
}
