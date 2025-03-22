// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable ClassWithVirtualMembersNeverInherited.Global
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
namespace DistribuTe.Domain.AppEntities;

using DeploymentEntities;

public class DeploymentStatusAggregate : BaseDeploymentStatus
{
    public int Id { get; set; }
    public virtual IList<DeploymentAggregate> Deployments { get; set; }
    public virtual IList<DeploymentItemAggregate> DeploymentItems { get; set; }
    public virtual IList<DeploymentItemTaskAggregate> DeploymentItemTasks { get; set; }
}