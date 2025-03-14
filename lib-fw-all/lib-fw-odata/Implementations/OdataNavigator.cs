namespace DistribuTe.Framework.OData.Implementations;

using Microsoft.AspNetCore.OData.Query;
using Microsoft.OData.UriParser;

public abstract class OdataNavigator<TModel, TEntity> : IOdataNavigator<TModel, TEntity>
    where TModel : new()
    where TEntity : new()
{
    public abstract IQueryable<TEntity> SafeApplyNavigationSource(ExpandedNavigationSelectItem selectedItem, IQueryable<TEntity> queryable);
    public IQueryable<TEntity> ApplyNavigations(ODataQueryOptions<TModel> queryOptions, IQueryable<TEntity> queryable)
    {
        if (queryOptions.SelectExpand?.SelectExpandClause?.SelectedItems?.Any() ?? false)
        {
            var selectExpandClause = queryOptions.SelectExpand.SelectExpandClause;
            var selectedItems = selectExpandClause.SelectedItems.Where(x => x is ExpandedNavigationSelectItem)
                .Select(x => x as ExpandedNavigationSelectItem)
                .Where(x => x.FilterOption != null);

            foreach (var selectedItem in selectedItems)
            {
                queryable = SafeApplyNavigationSource(selectedItem, queryable);
            }
        }

        return queryable;
    }
}