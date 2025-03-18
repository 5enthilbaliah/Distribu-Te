namespace DistribuTe.Framework.ApiEssentials.Odata.Implementations;

using AppEssentials;
using AppEssentials.Implementations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.OData.Extensions;
using Microsoft.AspNetCore.OData.Query;

public class OdataPaginator<TModel>(OdataFilterVisitor<WhereClauseItem> visitor) : IOdataPaginator
    where TModel : IModel, new()
{
    private readonly OdataFilterVisitor<WhereClauseItem> _visitor = visitor ??
                                                                    throw new ArgumentNullException(nameof(visitor));

    public virtual string Name => "base";
    
    public virtual async Task<long> CountAsync(CancellationToken cancellationToken = default)
    {
        return await Task.FromResult(0);
    }

    public ILinqQueryFacade<WhereClauseItem> GenerateWhereClauseFacadeFrom(HttpRequest httpRequest)
    {
        var path = httpRequest.ODataFeature().Path;
        var model = httpRequest.GetModel();
        var queryContext = new ODataQueryContext(model, typeof(TModel), path);
        var queryOptions = new ODataQueryOptions<TModel>(queryContext, httpRequest);

        queryOptions.Filter?.FilterClause.Expression.Accept(_visitor);

        return queryOptions.Filter == null
            ? new LinqQueryFacade(new List<WhereClauseItem>().AsReadOnly(), queryOptions.Skip?.Value,
                queryOptions.Top?.Value)
            : new LinqQueryFacade(_visitor.FilterOptions, queryOptions.Skip?.Value, queryOptions.Top?.Value);
    }
}