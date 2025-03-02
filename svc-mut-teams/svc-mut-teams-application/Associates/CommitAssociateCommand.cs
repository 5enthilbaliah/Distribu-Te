namespace DistribuTe.Mutators.Teams.Application.Associates;

using MediatR;
using Models;
using Shared;

public class CommitAssociateCommand : IRequest<AssociateVm>, IUserTrackable
{
    public int Id { get; set; }
    public AssociateRm Associate { get; set; } = null!;
    public string? User { get; set; }
}