namespace DistribuTe.Mutators.Teams.Application.SquadAssociates.Models;

public class SquadAssociateRm
{
    public int SquadId { get; set; }
    public int AssociateId { get; set; }
    public DateTime StartedOn { get; set; }
    public DateTime? EndedOn { get; set; }
    public decimal Capacity { get; set; }
}