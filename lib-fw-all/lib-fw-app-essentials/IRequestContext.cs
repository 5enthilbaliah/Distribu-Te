namespace DistribuTe.Framework.AppEssentials;

public interface IRequestContext
{
    string CorrelationId { get; }
    string UserIdentity { get; }
    string UserEmail { get; }
    string HttpMethod { get; }

    TFeature? GetFeature<TFeature>();
}