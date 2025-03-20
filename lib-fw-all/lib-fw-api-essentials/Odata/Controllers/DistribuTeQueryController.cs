namespace DistribuTe.Framework.ApiEssentials.Odata.Controllers;

using System.Collections.ObjectModel;
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

    private ReadOnlyCollection<WhereClauseItem> RetrieveWhereClauses(ODataQueryOptions<TModel> queryOptions)
    {
        var whereClauseItems = new List<WhereClauseItem>().AsReadOnly();
        if (queryOptions.Filter != null)
        {
            queryOptions.Filter.FilterClause.Expression.Accept(_visitor);
            whereClauseItems = _visitor.FilterOptions;
        }
        
        return whereClauseItems;
    }
    
    private ReadOnlyCollection<OrderByClauseItem> RetrieveOrderByClauses(ODataQueryOptions<TModel> queryOptions)
    {
        var orderByClauseItems = new List<OrderByClauseItem>().AsReadOnly();
        if (queryOptions.OrderBy is { OrderByNodes.Count: > 0 })
        {
            var orderByList = new List<OrderByClauseItem>();
            foreach (var orderBy in queryOptions.OrderBy.RawValue.ToLower().Split([',']))
            { 
                var split = orderBy.Split([' ']);

                if (split.Last() == "desc")
                {
                    orderByList.Add(OrderByClauseItem.SpawnOne(split.First(), SortDirections.Descending));
                    continue;
                }
                
                orderByList.Add(OrderByClauseItem.SpawnOne(split.First()));
            }

            orderByClauseItems = orderByList.AsReadOnly();
        }

        return orderByClauseItems;
    }
    
    protected EntityLinqFacade GenerateWhereClauseFacadeFrom(ODataQueryOptions<TModel> queryOptions)
    {
        var whereClauseItems = RetrieveWhereClauses(queryOptions);
        var orderByClauseItems = RetrieveOrderByClauses(queryOptions);
        
        var facade = new EntityLinqFacade(whereClauseItems, orderByClauseItems, 
            queryOptions.Skip?.Value, queryOptions.Top?.Value);
        
        _requestContext.Set(facade);
        return _navigator.ApplyNavigations(facade, queryOptions);
    }
}