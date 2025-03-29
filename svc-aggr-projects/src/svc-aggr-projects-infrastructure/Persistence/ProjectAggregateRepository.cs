namespace DistribuTe.Aggregates.Projects.Infrastructure.Persistence;

using Domain.Entities;

internal class ProjectAggregateRepository(ProjectSchemaDatabaseContext context) 
    : AggregateRepository<ProjectAggregate, ProjectId>(context)
{ }