namespace DistribuTe.Aggregates.Teams.Infrastructure.Persistence;

using Domain.Entities;

internal class SquadTeamsRepository(TeamDatabaseContext context) 
    : TeamsRepository<SquadAggregate, SquadId>(context)
{ }