namespace DistribuTe.Aggregates.Projects.Infrastructure.Persistence;

using Domain.Entities;

internal class ProjectCategoryAggregateRepository(ProjectSchemaDatabaseContext context) 
    : AggregateRepository<ProjectCategoryAggregate, ProjectCategoryId>(context)
{ }