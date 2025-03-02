namespace DistribuTe.Mutators.Teams.Infrastructure.Persistence;

using Domain.Entities;

internal sealed class SquadTeamsRepository(TeamDatabaseContext context) 
    : TeamsRepository<Squad, SquadId>(context)
{ }