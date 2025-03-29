namespace DistribuTe.Aggregates.Projects.Domain.Entities;

using DistribuTe.Domain.ProjectEntities;
using Framework.DomainEssentials;

public record ProjectCategoryId(int Value)
{
    public static bool operator>(ProjectCategoryId a, ProjectCategoryId b)
    {
        return a.Value > b.Value;
    }
    
    public static bool operator<(ProjectCategoryId a, ProjectCategoryId b)
    {
        return a.Value < b.Value;
    }
    
    public static bool operator>=(ProjectCategoryId a, ProjectCategoryId b)
    {
        return a.Value >= b.Value;
    }
    
    public static bool operator<=(ProjectCategoryId a, ProjectCategoryId b)
    {
        return a.Value <= b.Value;
    }
}

public class ProjectCategoryAggregate : BaseProjectCategory, IEntity<ProjectCategoryId>
{
    public ProjectCategoryId Id { get; set; }  = null!;
    
    public virtual ICollection<ProjectAggregate> Projects { get; set; }
}