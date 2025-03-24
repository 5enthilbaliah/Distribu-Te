namespace DistribuTe.Mutators.Projects.Application;

using Domain.Entities;

public interface ITeamsApiReader
{
    Task<bool> SquadExistsAsync(SquadId squadId, CancellationToken cancellationToken = default);
}