namespace DistribuTe.Framework.ApiEssentials.Odata.Controllers;

using System.Collections.ObjectModel;
using AppEssentials;
using AppEssentials.Implementations;
using Implementations;
using Microsoft.AspNetCore.OData.Query;

public class DistribuTeAggregateController<TModel>(
    OdataFilterVisitor<WhereClauseItem> visitor,
    IOdataNavigator<TModel, WhereClauseItem> navigator) : DistribuTeController
    where TModel : IModel, new()
{
    private readonly OdataFilterVisitor<WhereClauseItem> _visitor = visitor ??
                                                                    throw new ArgumentNullException(nameof(visitor));

    private readonly IOdataNavigator<TModel, WhereClauseItem> _navigator = navigator ??
                                                                           throw new ArgumentNullException(
                                                                               nameof(navigator));

    protected ILinqQueryFacade<WhereClauseItem> GenerateWhereClauseFacadeFrom(ODataQueryOptions<TModel> queryOptions)
    {
        if (queryOptions.Filter == null)
        {
            var emptyFacade = new LinqQueryFacade(new List<WhereClauseItem>().AsReadOnly(), queryOptions.Skip?.Value, queryOptions.Top?.Value);
            return _navigator.ApplyNavigations(emptyFacade, queryOptions);
        }
        
        queryOptions.Filter.FilterClause.Expression.Accept(_visitor);
        var facade = new LinqQueryFacade(visitor.FilterOptions, queryOptions.Skip?.Value, queryOptions.Top?.Value);
        
        return _navigator.ApplyNavigations(facade, queryOptions);
    }
}