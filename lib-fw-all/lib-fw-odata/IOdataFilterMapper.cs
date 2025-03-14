namespace DistribuTe.Framework.OData;

using System.Linq.Expressions;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.OData.UriParser;

public interface IOdataFilterMapper<TModel, TEntity>
    where TModel : new()
    where TEntity : new()
{
    Expression<Func<TEntity, bool>>? MapAsSearchExpression(ODataQueryOptions<TModel> oDataQueryOptions);
    Expression<Func<TEntity, bool>>? MapAsSearchExpression(FilterClause filterClause);

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