namespace DistribuTe.Aggregates.Teams.Infrastructure.Persistence;

using Domain.Entities;

internal class AssociateTeamsRepository(TeamDatabaseContext context) 
    : TeamsRepository<AssociateAggregate, AssociateId>(context)
{ }