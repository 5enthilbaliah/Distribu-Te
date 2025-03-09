namespace DistribuTe.Mutators.Teams.Application.Squads;

using ErrorOr;
using MediatR;

public class TrashSquadCommand : IRequest<ErrorOr<bool>>
{
    public int Id { get; set; }
}