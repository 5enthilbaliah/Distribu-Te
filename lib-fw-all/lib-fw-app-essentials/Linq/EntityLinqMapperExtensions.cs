namespace DistribuTe.Framework.AppEssentials.Linq;

using System.Collections.ObjectModel;
using System.Linq.Expressions;
using DomainEssentials;
using ErrorOr;

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

    public static IEnumerable<Error> ValidateFilters<TEntity, TId>(this EntityLinqMapper<TEntity, TId> entityMapper,
        ReadOnlyCollection<WhereClauseItem> whereClauses, string entityName)
        where TEntity : class, IEntity<TId>
        where TId : class
    {
        return whereClauses.Where(whereClause => !entityMapper.FilterMapExists(whereClause))
            .Select(whereClause => Error.Validation($"{entityName}.filter_not_found",
                $"Filter '{whereClause.FieldName} is not valid.'"));
    }
    
    public static IEnumerable<Error> ValidateSortOrders<TEntity, TId>(this EntityLinqMapper<TEntity, TId> entityMapper,
        ReadOnlyCollection<OrderByClauseItem> orderByClauses, string entityName)
        where TEntity : class, IEntity<TId>
        where TId : class
    {
        return orderByClauses.Where(orderByClause => !entityMapper.SortMapExists(orderByClause))
            .Select(orderByClause => Error.Validation($"{entityName}.filter_not_found",
                $"Filter '{orderByClause.FieldName} is not valid.'"));
    }
}