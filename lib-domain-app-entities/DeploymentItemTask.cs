// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable ClassWithVirtualMembersNeverInherited.Global
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
namespace DistribuTe.Domain.AppEntities;

public class DeploymentItemTask
{
    public int Id { get; set; }
    public int DeploymentItemId { get; set; }
    public int AssociateId { get; set; }
    public int Sequence { get; set; }
    public DateTime? ActualStart { get; set; }
    public DateTime? ActualEnd { get; set; }
    public string Comments { get; set; } = null!;
    public DateTime CreatedOn { get; set; }
    public string CreatedBy { get; set; } = null!;
    public DateTime? ModifiedOn { get; set; }
    public string? ModifiedBy { get; set; }
    public int StatusId { get; set; }
    public int TaskTypeId { get; set; }
}

public class DeploymentItemTaskAggregate : DeploymentItemTask
{
    public virtual DeploymentItemAggregate DeploymentItem { get; set; }
    public virtual AssociateAggregate Associate { get; set; }
    public virtual DeploymentStatusAggregate Status { get; set; }
    public virtual DeploymentTaskTypeAggregate TaskType { get; set; }
}