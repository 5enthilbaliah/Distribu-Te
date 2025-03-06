namespace DistribuTe.Domain.DeploymentEntities;

public class BaseDeploymentStatus
{
    public string Name { get; set; } = null!;
    public DateTime CreatedOn { get; set; }
    public string CreatedBy { get; set; } = null!;
    public DateTime? ModifiedOn { get; set; }
    public string? ModifiedBy { get; set; }
}