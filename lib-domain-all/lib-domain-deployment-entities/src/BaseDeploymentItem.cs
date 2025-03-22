namespace DistribuTe.Domain.DeploymentEntities;

public class BaseDeploymentItem
{
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
    public int StatusId { get; set; }
}