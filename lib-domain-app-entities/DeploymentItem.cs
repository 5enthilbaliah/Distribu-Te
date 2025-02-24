// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable ClassWithVirtualMembersNeverInherited.Global
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
namespace DistribuTe.Domain.AppEntities;

public class DeploymentItem
{
    public int Id { get; set; }
    public int DeploymentId { get; set; }
    public int ProjectId { get; set; }
    public int Sequence { get; set; }
    public DateTime? ActualStart { get; set; }
    public DateTime? ActualEnd { get; set; }
    public string Comments { get; set; } = null!;
    public DateTime CreatedOn { get; set; }
    public string CreatedBy { get; set; } = null!;
    public DateTime? ModifiedOn { get; set; }
    public string? ModifiedBy { get; set; }
}

public class DeploymentItemAggregate : DeploymentItem
{
    public virtual DeploymentAggregate Deployment { get; set; }
    public virtual ProjectAggregate Project { get; set; }
    public virtual IList<DeploymentItemTaskAggregate> DeploymentItemTasks { get; set; }
}