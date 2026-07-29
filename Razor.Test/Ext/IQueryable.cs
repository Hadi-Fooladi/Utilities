using System.Linq.Expressions;

namespace HaFT.Utilities.Razor.Test;

using Models;

static class IQueryableExt
{
	public static IQueryable<TEntity> SortBy<TEntity>(
		this IQueryable<TEntity> query,
		IEnumerable<SortRule> rules,
		Func<SortRule, Expression<Func<TEntity, object?>>> getPropertyExpressionByRule)
	{
		IOrderedQueryable<TEntity>? orderedQuery = null;

		foreach (var rule in rules)
		{
			var expr = getPropertyExpressionByRule(rule);

			orderedQuery = orderedQuery == null
				? query.Sort(rule.Direction, expr)
				: orderedQuery.Sort(rule.Direction, expr);
		}

		return orderedQuery ?? query;
	}

	static IOrderedQueryable<TEntity> Sort<TEntity, TProperty>(
		this IQueryable<TEntity> query,
		SortDirection sortDirection,
		Expression<Func<TEntity, TProperty>> propertyExpression)
		=> sortDirection == SortDirection.Ascending
			? query.OrderBy(propertyExpression)
			: query.OrderByDescending(propertyExpression);

	static IOrderedQueryable<TEntity> Sort<TEntity, TProperty>(
		this IOrderedQueryable<TEntity> query,
		SortDirection sortDirection,
		Expression<Func<TEntity, TProperty>> propertyExpression)
		=> sortDirection == SortDirection.Ascending
			? query.ThenBy(propertyExpression)
			: query.ThenByDescending(propertyExpression);
}
