namespace DistribuTe.Framework.ApiEssentials.Odata;

using AppEssentials;
using Implementations;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.OData.UriParser;

public interface IOdataNavigator<TModel, TWhereClause>
    where TModel : IModel, new()
    where TWhereClause : IWhereClause
{
    IWhereClauseFacade<TWhereClause> ApplyNavigations(IWhereClauseFacade<TWhereClause> facade, ODataQueryOptions<TModel> queryOptions);
}