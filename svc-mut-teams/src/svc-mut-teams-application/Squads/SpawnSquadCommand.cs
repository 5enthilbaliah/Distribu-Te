namespace DistribuTe.Mutators.Teams.Application.Squads;

using DataContracts;
using ErrorOr;
using MediatR;
using Shared;

public class SpawnSquadCommand : IRequest<ErrorOr<SquadResponse>>, IUserTrackable
{
    public SquadRequest Squad { get; set; } = null!;
    public string? User { get; set; }
}