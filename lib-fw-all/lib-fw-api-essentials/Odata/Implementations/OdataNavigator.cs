namespace DistribuTe.Framework.ApiEssentials.Odata.Implementations;

using AppEssentials;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.OData.UriParser;

public class OdataNavigator<TModel, TWhereClause>(Func<TWhereClause> generator) : IOdataNavigator<TModel, TWhereClause>
    where TModel : IModel, new()
    where TWhereClause : IWhereClause
{
    private readonly Func<TWhereClause> _generator = generator ?? throw new ArgumentNullException(nameof(generator));
    
    public IWhereClauseFacade<TWhereClause> ApplyNavigations(IWhereClauseFacade<TWhereClause> facade, ODataQueryOptions<TModel> queryOptions)
    {
        if (queryOptions.SelectExpand is not { SelectExpandClause: not null }
            || !queryOptions.SelectExpand.SelectExpandClause.SelectedItems.Any()) return facade;
        
        var selectExpandClause = queryOptions.SelectExpand.SelectExpandClause;
        var selectedItems = selectExpandClause.SelectedItems
            .OfType<ExpandedNavigationSelectItem>()
            .Where(x => x.FilterOption != null)
            .Where(x => x.NavigationSource != null);

        foreach (var selectedItem in selectedItems)
        {
            var visitor = new OdataFilterVisitor<TWhereClause>(_generator);
            selectedItem.FilterOption.Expression.Accept(visitor);
                
            facade.AddInnerWhereClauses(selectedItem.NavigationSource.Name.ToLower(),
                visitor.FilterOptions);
        }

        return facade;
    }
}