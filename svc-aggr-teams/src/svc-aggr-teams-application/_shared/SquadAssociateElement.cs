// ReSharper disable InconsistentNaming
// ReSharper disable once CheckNamespace
namespace DistribuTe.Aggregates.Teams.Application.Shared;

public class SquadAssociateElement
{
    public int Squad_Id { get; set; }
    public int Associate_Id { get; set; }
    public DateTime Started_On { get; set; }
    public DateTime? Ended_On { get; set; }
    public decimal Capacity { get; set; }
}