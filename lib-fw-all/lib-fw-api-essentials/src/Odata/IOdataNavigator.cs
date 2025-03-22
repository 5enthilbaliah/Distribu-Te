namespace DistribuTe.Framework.ApiEssentials.Odata;

using AppEssentials;
using AppEssentials.Linq;
using Microsoft.AspNetCore.OData.Query;

public interface IOdataNavigator<TModel>
    where TModel : IModel, new()
{
    EntityLinqFacade ApplyNavigations(EntityLinqFacade facade, ODataQueryOptions<TModel> queryOptions);
}