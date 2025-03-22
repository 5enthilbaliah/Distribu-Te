namespace DistribuTe.Aggregates.Teams.Infrastructure.Persistence;

using Domain.Entities;

internal class AssociateAggregateRepository(TeamSchemaDatabaseContext context) 
    : AggregateRepository<AssociateAggregate, AssociateId>(context)
{ }