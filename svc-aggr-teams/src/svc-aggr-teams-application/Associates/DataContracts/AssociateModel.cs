// ReSharper disable InconsistentNaming
namespace DistribuTe.Aggregates.Teams.Application.Associates.DataContracts;

using System.Text.Json.Serialization;
using Framework.AppEssentials;
using Shared;

public class AssociateModel : IModel
{
    public int Id { get; set; }
    public string First_Name { get; set; } = null!;
    public string Last_Name { get; set; } = null!;
    public string? Middle_Name { get; set; }
    public char Gender { get; set; }
    public string Email_Id { get; set; } = null!;
    
    public IList<SquadAssociateElement>? Squad_Associates { get; set; }

    [JsonIgnore]
    public string ModelIdentifier => $"{Id}";
}