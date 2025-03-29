// ReSharper disable InconsistentNaming
// ReSharper disable CheckNamespace
namespace DistribuTe.Aggregates.Projects.Application.Shared;

public class SquadProjectElement
{
    public int Squad_Id { get; set; }
    public int Project_Id { get; set; }
    public DateTime Started_On { get; set; }
    public DateTime? Ended_On { get; set; }
}