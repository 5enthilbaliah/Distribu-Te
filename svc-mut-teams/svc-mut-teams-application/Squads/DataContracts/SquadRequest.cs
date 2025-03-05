namespace DistribuTe.Mutators.Teams.Application.Squads.DataContracts;

public class SquadRequest
{
    public string Name { get; set; } = null!;
    public string Code { get; set; } = null!;
    public string? Description { get; set; }
}