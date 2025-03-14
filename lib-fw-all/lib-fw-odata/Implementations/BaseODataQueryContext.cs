namespace DistribuTe.Framework.OData.Implementations;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.OData.Extensions;
using Microsoft.AspNetCore.OData.Query;

public class BaseODataQueryContext<T>(IHttpContextAccessor httpContextAccessor) : IODataQueryContext<T>
    where T : class, new()
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));

    public virtual string Name { get; }

    public virtual async Task<long> CountAsync()
    {
        return await Task.FromResult(0);
    }

    public ODataQueryOptions<T> SpawnOdataQueryOptions()
    {
        return SpawnOdataQueryOptions(_httpContextAccessor.HttpContext!.Request);
    }

    public ODataQueryOptions<T> SpawnOdataQueryOptions(HttpRequest httpRequest)
    {
        var path = httpRequest.ODataFeature().Path;
        var model = httpRequest.GetModel();
        var queryContext = new ODataQueryContext(model, typeof(T), path);
        return new ODataQueryOptions<T>(queryContext, httpRequest);
    }
}