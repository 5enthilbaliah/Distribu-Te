// ReSharper disable InconsistentNaming
namespace DistribuTe.Aggregates.Teams.Application.Squads;

using Base;

public class SquadAggregate : SquadElement
{
    public IList<SquadAssociateElement>? Squad_Associates { get; set; }
}