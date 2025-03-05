namespace DistribuTe.Mutators.Teams.Application.Squads.DataContracts;

public class SquadResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Code { get; set; } = null!;
    public string? Description { get; set; }
}