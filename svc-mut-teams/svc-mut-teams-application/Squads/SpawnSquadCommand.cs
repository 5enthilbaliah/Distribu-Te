namespace DistribuTe.Mutators.Teams.Application.Squads;

using MediatR;
using Models;
using Shared;

public class SpawnSquadCommand : IRequest<SquadVm>, IUserTrackable
{
    public SquadRm Squad { get; set; } = null!;
    public string? User { get; set; }
}