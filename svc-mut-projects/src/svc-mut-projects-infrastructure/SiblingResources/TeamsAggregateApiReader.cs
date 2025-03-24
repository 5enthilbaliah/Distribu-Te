namespace DistribuTe.Mutators.Projects.Infrastructure.SiblingResources;

using System.Net;
using Application;
using Domain.Entities;
using Microsoft.Extensions.Caching.Memory;

public class TeamsAggregateApiReader(ITeamsAggregateApi teamsAggregateApi) : ITeamsAggregateApiReader
{
    private readonly ITeamsAggregateApi _teamsAggregateApi = teamsAggregateApi 
                                                             ?? throw new ArgumentNullException(nameof(teamsAggregateApi));
    public async Task<bool> SquadExistsAsync(SquadId squadId, string token, CancellationToken cancellationToken = default)
    {
        var response = await _teamsAggregateApi.FindSquadAsync(squadId.Value, token, cancellationToken);
        return response.StatusCode == HttpStatusCode.OK;
    }
}