namespace DistribuTe.Aggregates.Projects.Domain.Entities;

using DistribuTe.Domain.ProjectEntities;
using Framework.DomainEssentials;

public record SquadId(int Value)
{
    public static bool operator>(SquadId a, SquadId b)
    {
        return a.Value > b.Value;
    }
    
    public static bool operator<(SquadId a, SquadId b)
    {
        return a.Value < b.Value;
    }
    
    public static bool operator>=(SquadId a, SquadId b)
    {
        return a.Value >= b.Value;
    }
    
    public static bool operator<=(SquadId a, SquadId b)
    {
        return a.Value <= b.Value;
    }
}

public record SquadProjectId(SquadId SquadId, ProjectId ProjectId);

public class SquadProjectAggregate : BaseSquadProject, IEntity<SquadProjectId>
{
    public SquadProjectId Id { get; set; }  = null!;
    public SquadId SquadId { get; set; } = null!;
    public ProjectId ProjectId { get; set; }  = null!;
    
    public virtual ProjectAggregate Project { get; set; }
}