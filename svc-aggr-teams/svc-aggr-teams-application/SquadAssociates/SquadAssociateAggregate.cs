// ReSharper disable InconsistentNaming
namespace DistribuTe.Aggregates.Teams.Application.SquadAssociates;

using Base;

public class SquadAssociateAggregate : SquadAssociateElement
{
    public SquadElement? Squad { get; set; }
    public AssociateElement? Associate { get; set; }
}