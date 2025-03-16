namespace DistribuTe.Framework.AppEssentials;

using System.Linq.Expressions;
using DomainEssentials;

public interface IWhereClauseMapper<TEntity, TId>
    where TEntity : class, IEntity<TId>
    where TId : class
{
    Expression<Func<TEntity, bool>>? MapAsSearchExpression(IWhereClauseFacade? whereClauseFacade);
    
    Dictionary<string, Func<string, Expression<Func<TEntity, bool>>>> EqualityChecks { get; }
    Dictionary<string, Func<string, Expression<Func<TEntity, bool>>>> GreaterThanChecks { get; }
    Dictionary<string, Func<string, Expression<Func<TEntity, bool>>>> GreaterThanEqualChecks { get; }
    Dictionary<string, Func<string, Expression<Func<TEntity, bool>>>> LesserThanChecks { get; }
    Dictionary<string, Func<string, Expression<Func<TEntity, bool>>>> LesserThanEqualChecks { get; }
    Dictionary<string, Func<string, Expression<Func<TEntity, bool>>>> NotEqualChecks { get; }
    Dictionary<string, Func<string, Expression<Func<TEntity, bool>>>> StartsWithChecks { get; }
    Dictionary<string, Func<string, Expression<Func<TEntity, bool>>>> EndsWithChecks { get; }
    Dictionary<string, Func<string, Expression<Func<TEntity, bool>>>> ContainsChecks { get; }
}