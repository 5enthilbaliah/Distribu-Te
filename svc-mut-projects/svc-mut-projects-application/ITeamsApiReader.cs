namespace DistribuTe.Mutators.Projects.Application;

using Domain.Entities;

public interface ITeamsApiReader
{
    Task<bool> ProjectExistsAsync(SquadId squadId, CancellationToken cancellationToken = default);
}