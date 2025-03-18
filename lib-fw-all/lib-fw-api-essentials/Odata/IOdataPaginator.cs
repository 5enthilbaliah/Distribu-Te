namespace DistribuTe.Framework.ApiEssentials.Odata;

public interface IOdataPaginator
{
    string Name { get; }
    Task<long> CountAsync(CancellationToken cancellationToken = default);
}
