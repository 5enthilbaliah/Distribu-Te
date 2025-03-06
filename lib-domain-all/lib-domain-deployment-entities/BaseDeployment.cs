namespace DistribuTe.Domain.DeploymentEntities;

public class BaseDeployment
{
    public string Name { get; set; } = null!;
    public int SquadId { get; set; }
    public int EnvironmentId { get; set; }
    public DateTime PlannedStart { get; set; }
    public DateTime PlannedEnd { get; set; }
    public DateTime? ActualStart { get; set; }
    public DateTime? ActualEnd { get; set; }
    public string Comments { get; set; } = null!;
    public DateTime CreatedOn { get; set; }
    public string CreatedBy { get; set; } = null!;
    public DateTime? ModifiedOn { get; set; }
    public string? ModifiedBy { get; set; }
    public int StatusId { get; set; }
}