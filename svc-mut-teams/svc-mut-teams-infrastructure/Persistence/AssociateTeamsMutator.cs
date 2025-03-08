namespace DistribuTe.Mutators.Teams.Infrastructure.Persistence;

using Domain.Entities;

internal sealed class AssociateTeamsMutator(TeamDatabaseContext context) 
    : TeamsMutator<Associate, AssociateId>(context)
{ }

internal sealed class AssociateTeamsReader(TeamDatabaseContext context)
    : TeamsReader<Associate, AssociateId>(context)
{}