namespace DistribuTe.Framework.AppEssentials;

using Microsoft.AspNetCore.Http;

public interface IRequestContext
{
    string CorrelationId { get; }
    string UserIdentity { get; }
    string Token { get; }
    string UserEmail { get; }
    string HttpMethod { get; }

    HttpContext Current { get; }

    TFeature? GetFeature<TFeature>();
    void Set<TFeature>(TFeature instance);
}