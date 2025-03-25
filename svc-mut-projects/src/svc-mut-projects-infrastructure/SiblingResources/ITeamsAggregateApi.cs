namespace DistribuTe.Mutators.Projects.Infrastructure.SiblingResources;

using Refit;

public interface ITeamsAggregateApi
{
    public class HealthCheckInfo
    {
        public string Status { get; set; }
    }
    
    [Get("/protected/associates/{associateId}")]
    Task<HttpResponseMessage> FindSquadAsync(int associateId, [Authorize(scheme: "Bearer")] string token,
        CancellationToken cancellationToken = default);
    
    [Get("/health")]
    Task<ApiResponse<HealthCheckInfo>> GetHealthAsync(CancellationToken cancellationToken = default);
}