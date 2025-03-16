// ReSharper disable InconsistentNaming
namespace DistribuTe.Aggregates.Teams.Application.Squads.DataContracts;

using Base;
using Framework.AppEssentials;

public class SquadModel : IModel
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Code { get; set; } = null!;
    public string? Description { get; set; }
    
    public IList<SquadAssociateElement>? Squad_Associates { get; set; }

    public string ModelIdentifier => $"{Id}";
}