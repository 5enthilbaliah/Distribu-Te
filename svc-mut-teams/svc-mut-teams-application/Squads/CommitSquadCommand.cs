namespace DistribuTe.Mutators.Teams.Application.Squads;

using DataContracts;
using MediatR;
using Shared;

public class CommitSquadCommand : IRequest<SquadResponse>, IUserTrackable
{
    public int Id { get; set; }
    public SquadRequest Squad { get; set; } = null!;
    public string? User { get; set; }
}