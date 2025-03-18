namespace DistribuTe.Framework.ApiEssentials.Odata.Controllers;

using System.Diagnostics;
using AppEssentials;
using AppEssentials.Linq;
using Implementations;
using Microsoft.AspNetCore.OData.Query;

public class DistribuTeQueryController<TModel>(
    OdataFilterVisitor visitor, IOdataNavigator<TModel> navigator,
    IRequestContext requestContext) : DistribuTeController
    where TModel : IModel, new()
{
    private readonly OdataFilterVisitor _visitor = visitor ?? throw new ArgumentNullException(nameof(visitor));
    private readonly IOdataNavigator<TModel> _navigator = navigator ?? 
                                                          throw new ArgumentNullException(nameof(navigator));
    private readonly IRequestContext _requestContext = requestContext ?? 
                                                       throw new ArgumentNullException(nameof(requestContext));

    protected LinqQueryFacade GenerateWhereClauseFacadeFrom(ODataQueryOptions<TModel> queryOptions)
    {
        var facade = new LinqQueryFacade(new List<WhereClauseItem>().AsReadOnly(), queryOptions.Skip?.Value, queryOptions.Top?.Value);
        
        if (queryOptions.Filter != null)
        {
            queryOptions.Filter.FilterClause.Expression.Accept(_visitor);
            facade = new LinqQueryFacade(_visitor.FilterOptions, queryOptions.Skip?.Value, queryOptions.Top?.Value);
        }
        
        _requestContext.Set(facade);
        return _navigator.ApplyNavigations(facade, queryOptions);
    }
}