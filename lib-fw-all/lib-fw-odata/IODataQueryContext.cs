namespace DistribuTe.Framework.OData;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.OData.Query;

public interface IODataQueryContext
{
    string Name { get; }
    Task<long> CountAsync();
}
public interface IODataQueryContext<T> : IODataQueryContext where T : class, new()
{

    ODataQueryOptions<T> SpawnOdataQueryOptions();
    ODataQueryOptions<T> SpawnOdataQueryOptions(HttpRequest httpRequest);
}