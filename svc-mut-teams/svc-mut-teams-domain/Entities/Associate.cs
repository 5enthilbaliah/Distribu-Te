namespace DistribuTe.Mutators.Teams.Domain.Entities;

using DistribuTe.Domain.TeamEntities;

public record AssociateId(int Value);

public class Associate : BaseAssociate, IEntity<AssociateId>, IAuditableEntity
{
    public AssociateId Id { get; set; }  = null!;
}