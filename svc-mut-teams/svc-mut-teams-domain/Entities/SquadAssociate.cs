namespace DistribuTe.Mutators.Teams.Domain.Entities;

using DistribuTe.Domain.TeamEntities;

public record SquadAssociateId(SquadId SquadId, AssociateId AssociateId);

public class SquadAssociate : BaseSquadAssociate, IEntity<SquadAssociateId>, IAuditableEntity
{
    public SquadAssociateId Id { get; set; }

    public AssociateId AssociateId { get; set; }

    public SquadId SquadId { get; set; }
}