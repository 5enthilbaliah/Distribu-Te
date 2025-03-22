namespace DistribuTe.Mutators.Projects.Domain.Entities;

using DistribuTe.Domain.ProjectEntities;
using Framework.DomainEssentials;

public record ProjectCategoryId(int Value);

public class ProjectCategory : BaseProjectCategory, IEntity<ProjectCategoryId>, IAuditableEntity
{
    public ProjectCategoryId Id { get; set; } = null!;
}