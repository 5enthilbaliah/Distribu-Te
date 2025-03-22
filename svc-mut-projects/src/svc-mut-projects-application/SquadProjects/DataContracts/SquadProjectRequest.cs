// ReSharper disable InconsistentNaming
namespace DistribuTe.Mutators.Projects.Application.SquadProjects.DataContracts;

public class SquadProjectRequest
{
    public int Project_Id { get; set; }
    public int Squad_Id { get; set; }
    public DateTime Started_On { get; set; }
    public DateTime? Ended_On { get; set; }
}