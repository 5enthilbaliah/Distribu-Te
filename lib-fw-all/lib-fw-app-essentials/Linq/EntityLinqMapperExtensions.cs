namespace DistribuTe.Framework.AppEssentials.Linq;

using System.Linq.Expressions;

public static class EntityLinqMapperExtensions
{
    public static IQueryable<TEntity> SafeAscendingOrder<TEntity, TKey>(this IQueryable<TEntity> query, 
        Expression<Func<TEntity, TKey>> keySelector)
        where TEntity : class
    {
        return query.Expression.Type != typeof(IOrderedQueryable<TEntity>) ? query.OrderBy(keySelector) 
            : (query as IOrderedQueryable<TEntity>)!.ThenBy(keySelector);
    }
    
    public static IQueryable<TEntity> SafeDescendingOrder<TEntity, TKey>(this IQueryable<TEntity> query, 
        Expression<Func<TEntity, TKey>> keySelector)
        where TEntity : class
    {
        return query.Expression.Type != typeof(IOrderedQueryable<TEntity>) ? query.OrderByDescending(keySelector) 
            : (query as IOrderedQueryable<TEntity>)!.ThenByDescending(keySelector);
    }
}