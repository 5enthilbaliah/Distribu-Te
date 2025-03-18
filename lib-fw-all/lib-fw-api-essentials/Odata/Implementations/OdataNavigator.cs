namespace DistribuTe.Framework.ApiEssentials.Odata.Implementations;

using AppEssentials;
using AppEssentials.Linq;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.OData.UriParser;

public class OdataNavigator<TModel>(Func<WhereClauseItem> generator) : IOdataNavigator<TModel>
    where TModel : IModel, new()
{
    private readonly Func<WhereClauseItem> _generator = generator ?? throw new ArgumentNullException(nameof(generator));
    
    public LinqQueryFacade ApplyNavigations(LinqQueryFacade facade, ODataQueryOptions<TModel> queryOptions)
    {
        if (queryOptions.SelectExpand is not { SelectExpandClause: not null }
            || !queryOptions.SelectExpand.SelectExpandClause.SelectedItems.Any()) return facade;
        
        var selectExpandClause = queryOptions.SelectExpand.SelectExpandClause;
        var selectedItems = selectExpandClause.SelectedItems
            .OfType<ExpandedNavigationSelectItem>()
            //.Where(x => x.FilterOption != null)
            .Where(x => x.NavigationSource != null);

        foreach (var selectedItem in selectedItems)
        {
            var navigationSource = selectedItem.NavigationSource.Name.ToLower();
            if (selectedItem.FilterOption is not null)
            {
                var visitor = new OdataFilterVisitor(_generator);
                selectedItem.FilterOption.Expression.Accept(visitor);
                
                facade.AddInnerWhereClauses(navigationSource,
                    visitor.FilterOptions);
                
                continue;
            }
            
            facade.AddInnerWhereClauses(navigationSource, new List<WhereClauseItem>().AsReadOnly());
        }

        return facade;
    }
}