using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;
using Triplex.Validations;

namespace Boundaries.Persistance.Util;

public static class ExpressionBuilder
{
    private static readonly MethodInfo ContainsMethod = typeof(string).GetMethod("Contains", new Type[1]
    {
        typeof(string)
    })!;

    /// <summary>
    /// Gets an Entity expression to apply filters in Queryable Where method depending the filter sent.
    /// </summary>
    /// <typeparam name="TEntity">A db model</typeparam>
    /// <param name="criteria">An instance of <see cref="Criteria"/></param>
    /// <returns>An Expression that represents the operation</returns>
    public static Expression<Func<TEntity, bool>> GetExpression<TEntity>(Criteria criteria)
    {
        Arguments.NotNull(criteria, nameof(criteria));

        ParameterExpression parameterExpression = Expression.Parameter(typeof(TEntity), "t");
        MemberExpression left = BuildMemberExpression(criteria.PropertyName, parameterExpression);
        Expression right = BuildRightExpression(criteria, left);

        Expression methodCallExpression = criteria.Operation switch
        {
            Operation.GreaterThanOrEqual => Expression.GreaterThanOrEqual(left, right),
            Operation.LessThanOrEqual => Expression.LessThanOrEqual(left, right),
            Operation.Equals => Expression.Equal(left, right),
            Operation.NotEquals => Expression.NotEqual(left, right),
            _ => Expression.Call(left, ContainsMethod, right)
        };

        Expression<Func<TEntity, bool>> fullExpression = Expression.Lambda<Func<TEntity, bool>>(methodCallExpression!, parameterExpression);

        return fullExpression;
    }

    private static MemberExpression BuildMemberExpression(string propertyName, ParameterExpression parameterExpression)
    {
        Arguments.NotNullEmptyOrWhiteSpaceOnly(propertyName, nameof(propertyName));

        string[] properties = propertyName.Split('.');

        MemberExpression? expression = null;

        if (properties.Length == 1)
        {
            expression = Expression.Property(parameterExpression, propertyName);
        }
        else
        {
            for (int iteration = 0; iteration < properties.Length; iteration++)
            {
                string name = properties[iteration];

                bool isFirstIteration = iteration == 0 && expression is null;
                expression = isFirstIteration ? Expression.Property(parameterExpression, name) : Expression.Property(expression!, name);
            }
        }

        return expression!;
    }

    private static Expression BuildRightExpression(Criteria criteria, MemberExpression left)
    {
        Type propertyType = ((PropertyInfo)left.Member).PropertyType;
        TypeConverter converter = TypeDescriptor.GetConverter(propertyType);

        if (!converter.CanConvertFrom(typeof(string)))
            throw new NotSupportedException();

        object? propertyValue = converter.ConvertFromInvariantString(criteria.Value);
        ConstantExpression? constant = Expression.Constant(propertyValue);
        Expression right = Expression.Convert(constant, propertyType);

        return right;
    }
}