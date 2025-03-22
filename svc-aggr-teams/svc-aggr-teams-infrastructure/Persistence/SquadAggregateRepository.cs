namespace DistribuTe.Aggregates.Teams.Infrastructure.Persistence;

using Domain.Entities;

internal class SquadAggregateRepository(TeamSchemaDatabaseContext context) 
    : AggregateRepository<SquadAggregate, SquadId>(context)
{ }