namespace DistribuTe.Mutators.Teams.Application.SquadAssociates;

using DataContracts;
using ErrorOr;
using Framework.AppEssentials;
using MediatR;

public class SpawnSquadAssociateCommand : IRequest<ErrorOr<SquadAssociateResponse>>, IUserTrackable
{
    public SquadAssociateRequest SquadAssociate { get; set; } = null!;
    public string? User { get; set; }
}