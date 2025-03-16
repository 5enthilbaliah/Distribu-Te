namespace DistribuTe.Framework.AppEssentials;

using System.Linq.Expressions;

public static class LinqExpressionExtensions
{
    private static Expression<Func<T, bool>> Add<T>(this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
    {
        var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
        return Expression.Lambda<Func<T, bool>>
            (Expression.AndAlso(expr1.Body, invokedExpr), expr1.Parameters);
    }

    public static Expression<Func<T, bool>>? AsCombinedExpression<T>(this IEnumerable<Expression<Func<T, bool>>> expressions)
    {
        var enumerable = expressions as Expression<Func<T, bool>>[] ?? expressions.ToArray();
        
        if (enumerable.Length != 0)
            return enumerable.Aggregate((curr, next) => Add(curr, next));

        return null;
    }
}