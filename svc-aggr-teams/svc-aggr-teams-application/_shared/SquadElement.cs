// ReSharper disable InconsistentNaming
// ReSharper disable once CheckNamespace
namespace DistribuTe.Aggregates.Teams.Application.Shared;

public class SquadElement
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Code { get; set; } = null!;
    public string? Description { get; set; }
}