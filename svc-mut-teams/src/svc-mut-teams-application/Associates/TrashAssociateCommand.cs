namespace DistribuTe.Mutators.Teams.Application.Associates;

using ErrorOr;
using Framework.AppEssentials;
using MediatR;

public class TrashAssociateCommand : IRequest<ErrorOr<bool>>, IUserTrackable
{
    public int Id { get; set; }
    public string? User { get; set; }
}