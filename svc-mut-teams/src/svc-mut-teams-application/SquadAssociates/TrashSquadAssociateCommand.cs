namespace DistribuTe.Mutators.Teams.Application.SquadAssociates;

using ErrorOr;
using MediatR;

public class TrashSquadAssociateCommand : IRequest<ErrorOr<bool>>
{
    public int SquadId { get; set; }
    public int AssociateId { get; set; }
}