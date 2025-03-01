namespace DistribuTe.Mutators.Teams.Domain.Entities;

using DistribuTe.Domain.AppEntities;

public readonly record struct SquadAssociateId(SquadId SquadId, AssociateId AssociateId);

public class SquadAssociate : BaseSquadAssociate, IEntity<SquadAssociateId>
{
    public SquadAssociateId Id { get; set; }
}