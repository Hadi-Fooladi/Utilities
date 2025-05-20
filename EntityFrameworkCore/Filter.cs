using System;
using System.Linq;
using System.Reflection;

namespace HaFT.Utilities.EntityFrameworkCore;

public class Filter
{
	public IQueryable<TEntity> Apply<TEntity>(IQueryable<TEntity> query)
	{
		var type = GetType();
		var builder = new QueryBuilder<TEntity>(query);

		foreach (var p in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
		{
			var attr = p.GetCustomAttribute<CheckAttribute>();
			if (attr == null) continue;

			switch (attr.Value)
			{
			case FilterCheckEnum.Contains:
				builder.CheckContains(p.Name, value() as string);
				break;

			case FilterCheckEnum.Equiality:
				builder.CheckEquiality(p.Name, value());
				break;
			
			default:
				throw new Exception("Unexpected `Value` for `CheckAttribute`");
			}

			object? value() => p.GetValue(this);
		}

		return builder.Query;
	}
}
