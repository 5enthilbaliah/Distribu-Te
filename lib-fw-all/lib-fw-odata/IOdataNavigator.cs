namespace DistribuTe.Framework.OData;

using Microsoft.AspNetCore.OData.Query;

public interface IOdataNavigator<TDto, TEntity>
    where TDto : new()
    where TEntity : new()
{
    IQueryable<TEntity> ApplyNavigations(ODataQueryOptions<TDto> queryOptions, IQueryable<TEntity> queryable);
}