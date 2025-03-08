namespace DistribuTe.Mutators.Teams.Infrastructure.Persistence;

using Domain.Entities;

internal sealed class SquadTeamsMutator(TeamDatabaseContext context) 
    : TeamsMutator<Squad, SquadId>(context)
{ }

internal sealed class SquadTeamsReader(TeamDatabaseContext context)
    : TeamsReader<Squad, SquadId>(context)
{}