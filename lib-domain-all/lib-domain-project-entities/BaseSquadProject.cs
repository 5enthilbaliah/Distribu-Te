namespace DistribuTe.Domain.ProjectEntities;

public class BaseSquadProject
{
    public DateTime StartedOn { get; set; }
    public DateTime? EndedOn { get; set; }
    public DateTime CreatedOn { get; set; }
    public string CreatedBy { get; set; } = null!;
    public DateTime? ModifiedOn { get; set; }
    public string? ModifiedBy { get; set; }
}