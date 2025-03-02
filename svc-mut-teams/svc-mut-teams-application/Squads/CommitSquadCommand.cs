namespace DistribuTe.Mutators.Teams.Application.Squads;

using MediatR;
using Models;
using Shared;

public class CommitSquadCommand : IRequest<SquadVm>, IUserTrackable
{
    public int Id { get; set; }
    public SquadRm Squad { get; set; } = null!;
    public string? User { get; set; }
}