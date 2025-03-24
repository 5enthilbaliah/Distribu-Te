namespace DistribuTe.Mutators.Teams.Application.Associates;

using DataContracts;
using ErrorOr;
using Framework.AppEssentials;
using MediatR;

public class SpawnAssociateCommand : IRequest<ErrorOr<AssociateResponse>>, IUserTrackable
{
    public AssociateRequest Associate { get; set; } = null!;
    public string? User { get; set; }
}