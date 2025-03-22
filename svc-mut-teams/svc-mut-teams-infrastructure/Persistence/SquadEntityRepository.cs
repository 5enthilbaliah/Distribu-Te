namespace DistribuTe.Mutators.Teams.Infrastructure.Persistence;

using Domain.Entities;

internal sealed class SquadEntityRepository(TeamSchemaDatabaseContext context) 
    : EntityRepository<Squad, SquadId>(context)
{ }
