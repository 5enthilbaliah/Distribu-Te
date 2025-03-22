namespace DistribuTe.Domain.DeploymentEntities;

public class BaseDeploymentTaskType
{
    public string Name { get; set; } = null!;
    public string Code { get; set; } = null!;
    public DateTime CreatedOn { get; set; }
    public string CreatedBy { get; set; } = null!;
    public DateTime? ModifiedOn { get; set; }
    public string? ModifiedBy { get; set; }
}