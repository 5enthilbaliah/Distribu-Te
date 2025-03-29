// ReSharper disable InconsistentNaming
namespace DistribuTe.Aggregates.Projects.Application.SquadProjects.DataContracts;

using System.Text.Json.Serialization;
using Framework.AppEssentials;
using Shared;

public class SquadProjectModel : IModel
{
    public int Squad_Id { get; set; }
    public int Project_Id { get; set; }
    public DateTime Started_On { get; set; }
    public DateTime? Ended_On { get; set; }
    
    public ProjectElement? Project { get; set; }
    
    [JsonIgnore]
    public string ModelIdentifier  => $"{Squad_Id}-{Project_Id}";
}