// ReSharper disable InconsistentNaming
namespace DistribuTe.Mutators.Teams.Application.SquadAssociates.DataContracts;

public class SquadAssociateResponse
{
    public int Squad_Id { get; set; }
    public int Associate_Id { get; set; }
    public DateTime Started_On { get; set; }
    public DateTime? Ended_On { get; set; }
    public decimal Capacity { get; set; }
}