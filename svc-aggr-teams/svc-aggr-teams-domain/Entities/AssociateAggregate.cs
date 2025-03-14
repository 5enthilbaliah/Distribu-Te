namespace DistribuTe.Aggregates.Teams.Domain.Entities;

using DistribuTe.Domain.TeamEntities;

public record AssociateId(int Value);

public class AssociateAggregate : BaseAssociate, IEntity<AssociateId>
{
    public AssociateId Id { get; set; }  = null!;
    public virtual IList<SquadAssociateAggregate> SquadAssociates { get; set; }
}

