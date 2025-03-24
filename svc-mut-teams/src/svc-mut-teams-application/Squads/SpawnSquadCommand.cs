namespace DistribuTe.Mutators.Teams.Application.Squads;

using DataContracts;
using ErrorOr;
using Framework.AppEssentials;
using MediatR;

public class SpawnSquadCommand : IRequest<ErrorOr<SquadResponse>>, IUserTrackable
{
    public SquadRequest Squad { get; set; } = null!;
    public string? User { get; set; }
}