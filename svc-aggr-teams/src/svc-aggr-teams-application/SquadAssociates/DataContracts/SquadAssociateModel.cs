// ReSharper disable InconsistentNaming
namespace DistribuTe.Aggregates.Teams.Application.SquadAssociates.DataContracts;

using System.Text.Json.Serialization;
using Framework.AppEssentials;
using Shared;

public class SquadAssociateModel : IModel
{
    public int Squad_Id { get; set; }
    public int Associate_Id { get; set; }
    public DateTime Started_On { get; set; }
    public DateTime? Ended_On { get; set; }
    public decimal Capacity { get; set; }
    
    
    public SquadElement? Squad { get; set; }
    public AssociateElement? Associate { get; set; }

    [JsonIgnore]
    public string ModelIdentifier => $"{Squad_Id}-{Associate_Id}";
}