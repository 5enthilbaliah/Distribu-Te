// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable ClassWithVirtualMembersNeverInherited.Global
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
namespace DistribuTe.Domain.AppEntities;

using TeamEntities;

public class SquadAggregate : BaseSquad
{
    public int Id { get; set; }
    public virtual IList<SquadAssociateAggregate> SquadAssociates { get; set; }
    public virtual IList<SquadProjectAggregate> SquadProjects { get; set; }
    public virtual IList<DeploymentAggregate> Deployments { get; set; }
}