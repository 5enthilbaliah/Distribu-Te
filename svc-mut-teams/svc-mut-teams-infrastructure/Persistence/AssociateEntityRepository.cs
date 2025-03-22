namespace DistribuTe.Mutators.Teams.Infrastructure.Persistence;

using Domain.Entities;

internal sealed class AssociateEntityRepository(TeamSchemaDatabaseContext context) 
    : EntityRepository<Associate, AssociateId>(context)
{ }