namespace DistribuTe.Mutators.Teams.Application.Associates;

using DataContracts;
using ErrorOr;
using Framework.AppEssentials;
using MediatR;

public class CommitAssociateCommand : IRequest<ErrorOr<AssociateResponse>>, IUserTrackable
{
    public int Id { get; set; }
    public AssociateRequest Associate { get; set; } = null!;
    public string? User { get; set; }
}