namespace DistribuTe.Mutators.Projects.Application;

using Domain.Entities;

public interface ITeamsAggregateApiReader
{
    Task<bool> SquadExistsAsync(SquadId squadId, string token, CancellationToken cancellationToken = default);
}