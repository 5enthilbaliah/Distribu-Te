// ReSharper disable InconsistentNaming
namespace DistribuTe.Aggregates.Teams.Application.SquadAssociates;

using Base;
using Framework.AppEssentials;

public class SquadAssociateModel : IModel
{
    public int Squad_Id { get; set; }
    public int Associate_Id { get; set; }
    public DateTime Started_On { get; set; }
    public DateTime? Ended_On { get; set; }
    public decimal Capacity { get; set; }
    
    
    public SquadElement? Squad { get; set; }
    public AssociateElement? Associate { get; set; }

    public string ModelIdentifier => $"{Squad_Id}-{Associate_Id}";
}