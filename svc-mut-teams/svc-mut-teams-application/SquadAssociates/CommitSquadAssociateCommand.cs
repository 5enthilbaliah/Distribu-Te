namespace DistribuTe.Mutators.Teams.Application.SquadAssociates;

using DataContracts;
using MediatR;
using Shared;

public class CommitSquadAssociateCommand : IRequest<SquadAssociateResponse>, IUserTrackable
{
    public int SquadId { get; set; }
    public int AssociateId { get; set; }
    public SquadAssociateRequest SquadAssociate { get; set; } = null!;
    public string? User { get; set; }
}