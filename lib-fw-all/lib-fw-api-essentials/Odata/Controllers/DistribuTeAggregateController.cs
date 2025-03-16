namespace DistribuTe.Framework.ApiEssentials.Odata.Controllers;

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

    protected IWhereClauseFacade<WhereClauseItem> GenerateWhereClauseFacadeFrom(ODataQueryOptions<TModel> queryOptions)
    {
        queryOptions.Filter.FilterClause.Expression.Accept(_visitor);
        var facade = new WhereClauseFacade(visitor.FilterOptions);
        
        return _navigator.ApplyNavigations(facade, queryOptions);
    }
}