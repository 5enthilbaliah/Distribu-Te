// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable ClassWithVirtualMembersNeverInherited.Global
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
namespace DistribuTe.Domain.AppEntities;

using DeploymentEntities;

public class DeploymentItemTaskAggregate : BaseDeploymentItemTask
{
    public int Id { get; set; }
    public virtual DeploymentItemAggregate DeploymentItem { get; set; }
    public virtual AssociateAggregate Associate { get; set; }
    public virtual DeploymentStatusAggregate Status { get; set; }
    public virtual DeploymentTaskTypeAggregate TaskType { get; set; }
}