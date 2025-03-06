namespace DistribuTe.Domain.TeamEntities;

public class BaseSquadAssociate
{
    public DateTime StartedOn { get; set; }
    public DateTime? EndedOn { get; set; }
    public decimal Capacity { get; set; }
    public DateTime CreatedOn { get; set; }
    public string CreatedBy { get; set; } = null!;
    public DateTime? ModifiedOn { get; set; }
    public string? ModifiedBy { get; set; }
}