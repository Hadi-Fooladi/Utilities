using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

namespace HaFT.Utilities.EntityFrameworkCore;

public class Filter
{
	public IQueryable<TEntity> Apply<TEntity>(IQueryable<TEntity> query)
	{
		var builder = new QueryBuilder<TEntity>(query);

		foreach (var (p, attr) in Attributes)
		{
			switch (attr)
			{
			case FilterCheckEnum.Contains:
				builder.CheckContains(p.Name, value() as string);
				break;

			case FilterCheckEnum.Equiality:
				builder.CheckEquiality(p.Name, value());
				break;
			}

			object? value() => p.GetValue(this);
		}

		return builder.Query;
	}

	public IEnumerable<string> FiltersTextSequence
	{
		get
		{
			foreach (var (p, attr) in Attributes)
			{
				var op = attr switch
				{
					FilterCheckEnum.Contains => "Contains",
					FilterCheckEnum.Equiality => "=",
					_ => "???"
				};

				yield return $"{p.Name} {op} '{p.GetValue(this)}'";
			}
		}
	}

	IEnumerable<(PropertyInfo property, FilterCheckEnum attr)> Attributes
	{
		get
		{
			foreach (var p in GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
			{
				var attr = p.GetCustomAttribute<CheckAttribute>();
				if (attr == null) continue;

				yield return attr.Value switch
				{
					FilterCheckEnum.Contains or FilterCheckEnum.Equiality => (p, attr.Value),
					_ => throw new Exception("Unexpected `Value` for `CheckAttribute`")
				};
			}
		}
	}
}
