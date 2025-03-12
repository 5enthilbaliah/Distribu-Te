namespace DistribuTe.Mutators.Teams.Infrastructure.Persistence;

using Domain.Entities;

internal sealed class AssociateTeamsRepository(TeamDatabaseContext context) 
    : TeamsRepository<Associate, AssociateId>(context)
{ }