namespace DistribuTe.Aggregates.Teams.Domain.Entities;

using DistribuTe.Domain.TeamEntities;
using Framework.DomainEssentials;

public record AssociateId(int Value)
{
    public static bool operator>(AssociateId a, AssociateId b)
    {
        return a.Value > b.Value;
    }
    
    public static bool operator<(AssociateId a, AssociateId b)
    {
        return a.Value < b.Value;
    }
    
    public static bool operator>=(AssociateId a, AssociateId b)
    {
        return a.Value >= b.Value;
    }
    
    public static bool operator<=(AssociateId a, AssociateId b)
    {
        return a.Value <= b.Value;
    }
}

public class AssociateAggregate : BaseAssociate, IEntity<AssociateId>
{
    public AssociateId Id { get; set; }  = null!;
    public virtual IList<SquadAssociateAggregate> SquadAssociates { get; set; }
}

