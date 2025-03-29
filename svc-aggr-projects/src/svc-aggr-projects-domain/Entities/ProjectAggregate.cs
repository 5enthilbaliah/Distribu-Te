namespace DistribuTe.Aggregates.Projects.Domain.Entities;

using DistribuTe.Domain.ProjectEntities;
using Framework.DomainEssentials;

public record ProjectId(int Value)
{
    public static bool operator>(ProjectId a, ProjectId b)
    {
        return a.Value > b.Value;
    }
    
    public static bool operator<(ProjectId a, ProjectId b)
    {
        return a.Value < b.Value;
    }
    
    public static bool operator>=(ProjectId a, ProjectId b)
    {
        return a.Value >= b.Value;
    }
    
    public static bool operator<=(ProjectId a, ProjectId b)
    {
        return a.Value <= b.Value;
    }
}

public class ProjectAggregate : BaseProject, IEntity<ProjectId>
{
    public ProjectId Id { get; set; } = null!;
    public ProjectCategoryId CategoryId  { get; set; } = null!;
    
    public virtual ProjectCategoryAggregate Category { get; set; } 
    public virtual IList<SquadProjectAggregate> SquadProjects { get; set; }
}