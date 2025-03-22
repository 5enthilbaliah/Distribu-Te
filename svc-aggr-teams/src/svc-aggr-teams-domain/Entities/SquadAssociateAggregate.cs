namespace DistribuTe.Aggregates.Teams.Domain.Entities;

using DistribuTe.Domain.TeamEntities;
using Framework.DomainEssentials;

public record SquadAssociateId(SquadId SquadId, AssociateId AssociateId);

public class SquadAssociateAggregate : BaseSquadAssociate, IEntity<SquadAssociateId>
{
    public SquadAssociateId Id { get; set; } = null!;

    public AssociateId AssociateId { get; set; } = null!;

    public SquadId SquadId { get; set; } = null!;
    
    public virtual SquadAggregate Squad { get; set; }
    public virtual AssociateAggregate Associate { get; set; }
}