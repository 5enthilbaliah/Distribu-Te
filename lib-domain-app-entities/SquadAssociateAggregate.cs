// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable ClassWithVirtualMembersNeverInherited.Global
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
namespace DistribuTe.Domain.AppEntities;

using TeamEntities;

public class SquadAssociateAggregate : BaseSquadAssociate
{
    public int AssociateId { get; set; }
    public int SquadId { get; set; }
    public virtual SquadAggregate Squad { get; set; }
    public virtual AssociateAggregate Associate { get; set; }
}