// ReSharper disable once CheckNamespace
namespace DistribuTe.Mutators.Projects.Application.Shared;

public interface IRequestContext
{
    string CorrelationId { get; }
    string UserIdentity { get; }
    string UserEmail { get; }
    string HttpMethod { get; }

    TFeature? GetFeature<TFeature>();
}