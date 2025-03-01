namespace DistribuTe.Mutators.Teams.Domain.Entities;

using DistribuTe.Domain.AppEntities;

public readonly record struct AssociateId(int Value);

public class Associate : BaseAssociate, IEntity<AssociateId>
{
    public AssociateId Id { get; set; }
}