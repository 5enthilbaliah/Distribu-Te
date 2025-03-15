// ReSharper disable InconsistentNaming
namespace DistribuTe.Aggregates.Teams.Application.Associates;

using Base;

public class AssociateAggregate : AssociateElement
{
    public IList<SquadAssociateElement>? Squad_Associates { get; set; }
}