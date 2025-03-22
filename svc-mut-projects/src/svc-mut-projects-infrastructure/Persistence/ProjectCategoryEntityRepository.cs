namespace DistribuTe.Mutators.Projects.Infrastructure.Persistence;

using Domain.Entities;

internal sealed class ProjectCategoryEntityRepository(ProjectSchemaDatabaseContext context) 
    : EntityRepository<ProjectCategory, ProjectCategoryId>(context)
{ }