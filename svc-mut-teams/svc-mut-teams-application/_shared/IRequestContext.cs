// ReSharper disable once CheckNamespace
namespace DistribuTe.Mutators.Teams.Application.Shared;

public interface IRequestContext
{
    string CorrelationId { get; }
    string UserIdentity { get; }
    string UserEmail { get; }
    public string HttpMethod { get; }
}