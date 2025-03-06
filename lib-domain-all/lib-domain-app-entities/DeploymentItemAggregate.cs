// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable ClassWithVirtualMembersNeverInherited.Global
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
namespace DistribuTe.Domain.AppEntities;

using DeploymentEntities;

public class DeploymentItemAggregate : BaseDeploymentItem
{
    public int Id { get; set; }
    public virtual DeploymentAggregate Deployment { get; set; }
    public virtual ProjectAggregate Project { get; set; }
    public virtual IList<DeploymentItemTaskAggregate> DeploymentItemTasks { get; set; }
    public virtual DeploymentStatusAggregate Status { get; set; }
}