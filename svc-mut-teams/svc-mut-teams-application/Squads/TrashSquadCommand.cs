namespace DistribuTe.Mutators.Teams.Application.Squads;

using MediatR;

public class TrashSquadCommand : IRequest<bool>
{
    public int Id { get; set; }
}