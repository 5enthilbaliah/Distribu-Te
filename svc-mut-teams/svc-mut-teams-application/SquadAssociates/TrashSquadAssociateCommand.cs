namespace DistribuTe.Mutators.Teams.Application.SquadAssociates;

using MediatR;

public class TrashSquadAssociateCommand : IRequest<bool>
{
    public int SquadId { get; set; }
    public int AssociateId { get; set; }
}