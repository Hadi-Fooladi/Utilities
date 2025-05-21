using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

namespace HaFT.Utilities.EntityFrameworkCore;

/// <summary>
/// Note: Null values and empty strings are ignored.
/// </summary>
public class Filter
{
	public IQueryable<TEntity> Apply<TEntity>(IQueryable<TEntity> query)
	{
		var builder = new QueryBuilder<TEntity>(query);

		foreach (var (p, attr, value) in Attributes)
		{
			switch (attr.Value)
			{
			case FilterCheckEnum.Contains:
				builder.CheckContains(p.Name, value as string);
				break;

			case FilterCheckEnum.Equiality:
				builder.CheckEquiality(p.Name, value);
				break;
			}
		}

		return builder.Query;
	}

	public IEnumerable<string> FiltersTextSequence
	{
		get
		{
			foreach (var (p, attr, value) in Attributes)
			{
				var op = attr.Value switch
				{
					FilterCheckEnum.Contains => "Contains",
					FilterCheckEnum.Equiality => "=",
					_ => "???"
				};

				yield return $"{attr.Name ?? p.Name} {op} '{value}'";
			}
		}
	}

	IEnumerable<(PropertyInfo property, CheckAttribute attr, object value)> Attributes
	{
		get
		{
			foreach (var p in GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
			{
				var attr = p.GetCustomAttribute<CheckAttribute>();
				if (attr == null) continue;

				var value = p.GetValue(this);
				switch (value)
				{
				case null:
				case string s when string.IsNullOrEmpty(s):
					continue;

				default:
					yield return attr.Value switch
					{
						FilterCheckEnum.Contains or FilterCheckEnum.Equiality => (p, attr, value),
						_ => throw new Exception("Unexpected `Value` for `CheckAttribute`")
					};
					break;
				}
			}
		}
	}
}
