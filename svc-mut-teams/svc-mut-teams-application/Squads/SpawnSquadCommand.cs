namespace DistribuTe.Mutators.Teams.Application.Squads;

using DataContracts;
using MediatR;
using Shared;

public class SpawnSquadCommand : IRequest<SquadResponse>, IUserTrackable
{
    public SquadRequest Squad { get; set; } = null!;
    public string? User { get; set; }
}