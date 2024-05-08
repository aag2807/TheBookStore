using System.Linq.Expressions;
using Core.Boundaries.Persistance.Util;
using Triplex.Validations;
using ExpressionBuilder = Core.Boundaries.Persistance.Util.ExpressionBuilder;

namespace Boundaries.Persistance.Extensions;

public static class DbSetExtensions
{
    /// <summary>
    /// Order by a specified path
    /// </summary>
    /// <param name="source">The source of IQueryable</param>
    /// <param name="columnPath">The path of the column</param>
    /// <returns>A <see cref="IQueryable"/> sorted by property passed</returns>
    public static IOrderedQueryable<T> OrderByColumn<T>(this IQueryable<T> source, string columnPath)
        => source.OrderByColumnUsing(columnPath, "OrderBy");

    /// <summary>
    /// Order by a specified path
    /// </summary>
    /// <param name="source">The source of IQueryable</param>
    /// <param name="columnPath">The path of the column</param>
    /// <returns>A <see cref="IQueryable"/> sorted by property passed</returns>
    public static IOrderedQueryable<T> OrderByColumnDescending<T>(this IQueryable<T> source, string columnPath)
        => source.OrderByColumnUsing(columnPath, "OrderByDescending");

    /// <summary>
    /// Applies an order by depending the parameter
    /// </summary>
    /// <param name="entities">A <see cref="IQueryable"/> that contains the current query to be filtered</param>
    /// <param name="orderBy">The property name to be ordered ascending</param>
    /// <param name="orderByDescending">The property name to be ordered descending</param>
    /// <returns>A <see cref="IQueryable"/> sorted by property passed</returns>
    public static IQueryable<TEntity> OrderByPropertyName<TEntity>(this IQueryable<TEntity> entities, string? orderBy, string? orderByDescending)
    {
        if (!string.IsNullOrEmpty(orderBy))
        {
            entities = entities.OrderByColumn(orderBy);
        }

        if (!string.IsNullOrEmpty(orderByDescending))
        {
            entities = entities.OrderByColumnDescending(orderByDescending);
        }

        return entities;
    }

    private static IOrderedQueryable<T> OrderByColumnUsing<T>(this IQueryable<T> source, string columnPath, string method)
    {
        ParameterExpression parameter = Expression.Parameter(typeof(T), "item");
        Expression member = columnPath.Split('.')
            .Aggregate((Expression)parameter, Expression.PropertyOrField);
        LambdaExpression keySelector = Expression.Lambda(member, parameter);
        MethodCallExpression methodCall = Expression.Call(typeof(Queryable), method, new[]
                { parameter.Type, member.Type },
            source.Expression, Expression.Quote(keySelector));

        return (IOrderedQueryable<T>)source.Provider.CreateQuery(methodCall);
    }

    /// <summary>
    /// Applies a list of instances of <see cref="Criteria"/> to a Queryable and returns it filtered
    /// </summary>
    /// <param name="entities">The query to be filtered</param>
    /// <param name="criterias">All criterias that have to be applied</param>
    /// <returns>The queryable filtered</returns>
    public static IQueryable<TEntity> ApplyCriterias<TEntity>(this IQueryable<TEntity> entities, IList<Criteria> criterias)
    {
        Arguments.NotNull(entities, nameof(entities));
        Arguments.NotNull(criterias, nameof(criterias));

        foreach (var criteria in criterias)
        {
            Expression<Func<TEntity, bool>> expression = ExpressionBuilder.GetExpression<TEntity>(criteria);

            entities = entities.Where(expression);
        }

        return entities;
    }
}