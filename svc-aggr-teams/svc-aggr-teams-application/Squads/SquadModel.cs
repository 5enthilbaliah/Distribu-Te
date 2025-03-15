// ReSharper disable InconsistentNaming
namespace DistribuTe.Aggregates.Teams.Application.Squads;

using Base;

public class SquadModel
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Code { get; set; } = null!;
    public string? Description { get; set; }
    
    public IList<SquadAssociateElement>? Squad_Associates { get; set; }
}