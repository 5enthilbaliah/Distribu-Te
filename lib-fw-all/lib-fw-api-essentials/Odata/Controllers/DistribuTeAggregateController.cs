namespace DistribuTe.Framework.ApiEssentials.Odata.Controllers;

using AppEssentials;
using AppEssentials.Linq;
using Implementations;
using Microsoft.AspNetCore.OData.Query;

public class DistribuTeAggregateController<TModel>(
    OdataFilterVisitor visitor,
    IOdataNavigator<TModel> navigator) : DistribuTeController
    where TModel : IModel, new()
{
    private readonly OdataFilterVisitor _visitor = visitor ?? throw new ArgumentNullException(nameof(visitor));
    private readonly IOdataNavigator<TModel> _navigator = navigator ?? 
                                                          throw new ArgumentNullException(nameof(navigator));

    protected LinqQueryFacade GenerateWhereClauseFacadeFrom(ODataQueryOptions<TModel> queryOptions)
    {
        if (queryOptions.Filter == null)
        {
            var emptyFacade = new LinqQueryFacade(new List<WhereClauseItem>().AsReadOnly(), queryOptions.Skip?.Value, queryOptions.Top?.Value);
            return _navigator.ApplyNavigations(emptyFacade, queryOptions);
        }
        
        queryOptions.Filter.FilterClause.Expression.Accept(_visitor);
        var facade = new LinqQueryFacade(_visitor.FilterOptions, queryOptions.Skip?.Value, queryOptions.Top?.Value);
        
        return _navigator.ApplyNavigations(facade, queryOptions);
    }
}