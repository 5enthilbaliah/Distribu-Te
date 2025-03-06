// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable ClassWithVirtualMembersNeverInherited.Global
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
namespace DistribuTe.Domain.AppEntities;

using DeploymentEntities;

public class DeploymentAggregate : BaseDeployment
{
    public int Id { get; set; }
    public virtual SquadAggregate Squad { get; set; }
    public virtual EnvironmentAggregate Environment { get; set; }
    public virtual IList<DeploymentItemAggregate> DeploymentItems { get; set; }
    public virtual DeploymentStatusAggregate Status { get; set; }
}