namespace DistribuTe.Framework.AppEssentials.Linq;

using System.Collections.ObjectModel;
using System.Linq.Expressions;
using DomainEssentials;

public abstract class EntityLinqMapper<TEntity, TId>
    where TEntity : class, IEntity<TId>
    where TId : class
{
    protected abstract Dictionary<string, Func<string, Expression<Func<TEntity, bool>>>> EqualChecks { get; }
    protected abstract Dictionary<string, Func<string, Expression<Func<TEntity, bool>>>> GreaterThanChecks { get; }
    protected abstract Dictionary<string, Func<string, Expression<Func<TEntity, bool>>>> GreaterThanEqualChecks { get; }
    protected abstract Dictionary<string, Func<string, Expression<Func<TEntity, bool>>>> LesserThanChecks { get; }
    protected abstract Dictionary<string, Func<string, Expression<Func<TEntity, bool>>>> LesserThanEqualChecks { get; }
    protected abstract Dictionary<string, Func<string, Expression<Func<TEntity, bool>>>> NotEqualChecks { get; }
    protected abstract Dictionary<string, Func<string, Expression<Func<TEntity, bool>>>> StartsWithChecks { get; }
    protected abstract Dictionary<string, Func<string, Expression<Func<TEntity, bool>>>> EndsWithChecks { get; }
    protected abstract Dictionary<string, Func<string, Expression<Func<TEntity, bool>>>> ContainsChecks { get; }

    protected abstract Dictionary<string, Func<IQueryable<TEntity>, IQueryable<TEntity>>> AscendingSorters { get; }
    protected abstract Dictionary<string, Func<IQueryable<TEntity>, IQueryable<TEntity>>> DescendingSorters { get; }
    
    public Expression<Func<TEntity, bool>>? MapAsSearchExpression(ReadOnlyCollection<WhereClauseItem> whereClauses) 
    {
        var expressions = new List<Expression<Func<TEntity, bool>>>();

        foreach (var whereClause in whereClauses)
        {
            // ReSharper disable once SwitchStatementHandlesSomeKnownEnumValuesWithDefault
            switch (whereClause.Operator)
            {
                case Operators.EqualTo:
                    expressions.Add(EqualChecks[whereClause.FieldName!](whereClause.Value!));
                    break;
                case Operators.NotEqualTo:
                    expressions.Add(NotEqualChecks[whereClause.FieldName!](whereClause.Value!));
                    break;
                case Operators.GreaterThan:
                    expressions.Add(GreaterThanChecks[whereClause.FieldName!](whereClause.Value!));
                    break;
                case Operators.GreaterThanOrEqualTo:
                    expressions.Add(GreaterThanEqualChecks[whereClause.FieldName!](whereClause.Value!));
                    break;
                case Operators.LessThan:
                    expressions.Add(LesserThanChecks[whereClause.FieldName!](whereClause.Value!));
                    break;
                case Operators.LessThanOrEqualTo:
                    expressions.Add(LesserThanEqualChecks[whereClause.FieldName!](whereClause.Value!));
                    break;
                case Operators.StartsWith:
                    expressions.Add(StartsWithChecks[whereClause.FieldName!](whereClause.Value!));
                    break;
                case Operators.EndsWith:
                    expressions.Add(EndsWithChecks[whereClause.FieldName!](whereClause.Value!));
                    break;
                case Operators.Contains:
                    expressions.Add(ContainsChecks[whereClause.FieldName!](whereClause.Value!));
                    break;
            }
        }
        
        return expressions.Count != 0 ? expressions.AsCombinedExpression() : null;
    }
    
    public Expression<Func<TEntity, bool>>? MapAsSearchExpression(EntityLinqFacade? whereClauseFacade)
    {
        if (whereClauseFacade == null || whereClauseFacade.WhereClauses.Count == 0)
            return null;
        
        return MapAsSearchExpression(whereClauseFacade.WhereClauses);
    }

    public IQueryable<TEntity> ApplySort(IQueryable<TEntity> queryable, 
        ReadOnlyCollection<OrderByClauseItem> orderByClauses)
    {
        foreach (var orderByClause in orderByClauses)
        {
            if (orderByClause.Direction == SortDirections.Ascending)
            {
                queryable = AscendingSorters[orderByClause.FieldName](queryable);
                continue;
            }
            
            queryable = DescendingSorters[orderByClause.FieldName](queryable);
        }
        
        return queryable;
    }
}