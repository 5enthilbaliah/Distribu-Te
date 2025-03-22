namespace DistribuTe.Mutators.Projects.Domain.Entities;

using DistribuTe.Domain.ProjectEntities;
using Framework.DomainEssentials;

public record ProjectId(int Value);

public class Project : BaseProject, IEntity<ProjectId>, IAuditableEntity
{
    public ProjectId Id { get; set; }  = null!;
    public ProjectCategoryId CategoryId { get; set; } = null!;
}