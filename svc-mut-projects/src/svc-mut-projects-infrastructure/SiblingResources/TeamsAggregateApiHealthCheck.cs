namespace DistribuTe.Mutators.Projects.Infrastructure.SiblingResources;

using System.Net;
using Microsoft.Extensions.Diagnostics.HealthChecks;

public class TeamsAggregateApiHealthCheck(ITeamsAggregateApi teamsAggregateApi)  : IHealthCheck
{
    private readonly ITeamsAggregateApi _teamsAggregateApi = teamsAggregateApi 
                                                             ?? throw new ArgumentNullException(nameof(teamsAggregateApi));
    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, 
        CancellationToken cancellationToken = new())
    {
        try
        {
            var response = await _teamsAggregateApi.GetHealthAsync(cancellationToken);
            if (response.StatusCode == HttpStatusCode.OK && response.Content!.Status.Equals("Healthy"))
            {
                return HealthCheckResult.Healthy();
            }
            
            return HealthCheckResult.Unhealthy($"Api health check failed with status code {response.StatusCode}");
        }
        catch (Exception ex)
        {
            return HealthCheckResult.Unhealthy(ex.Message, ex);
        }
    }
}