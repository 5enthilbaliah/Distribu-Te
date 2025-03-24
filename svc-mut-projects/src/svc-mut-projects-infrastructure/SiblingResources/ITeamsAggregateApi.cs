namespace DistribuTe.Mutators.Projects.Infrastructure.SiblingResources;

using Refit;

public interface ITeamsAggregateApi
{
    [Get("/associates/{associateId}")]
    Task<HttpResponseMessage> FindSquadAsync(int associateId, [Authorize(scheme: "Bearer")] string token,
        CancellationToken cancellationToken = default);
}