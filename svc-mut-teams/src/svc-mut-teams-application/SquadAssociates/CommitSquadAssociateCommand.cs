namespace DistribuTe.Mutators.Teams.Application.SquadAssociates;

using DataContracts;
using ErrorOr;
using Framework.AppEssentials;
using MediatR;

public class CommitSquadAssociateCommand : IRequest<ErrorOr<SquadAssociateResponse>>, IUserTrackable
{
    public int SquadId { get; set; }
    public int AssociateId { get; set; }
    public SquadAssociateRequest SquadAssociate { get; set; } = null!;
    public string? User { get; set; }
}