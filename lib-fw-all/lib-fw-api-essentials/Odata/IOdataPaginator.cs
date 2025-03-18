namespace DistribuTe.Framework.ApiEssentials.Odata;

using AppEssentials;
using AppEssentials.Implementations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.OData.Query;

public interface IOdataPaginator
{
    string Name { get; }
    Task<long> CountAsync(CancellationToken cancellationToken = default);
}
