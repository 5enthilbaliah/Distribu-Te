namespace DistribuTe.Mutators.Projects.Infrastructure.Persistence;

using Domain.Entities;

internal sealed class ProjectEntityRepository(ProjectSchemaDatabaseContext context) 
    : EntityRepository<Project, ProjectId>(context)
{ }