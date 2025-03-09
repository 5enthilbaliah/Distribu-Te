namespace DistribuTe.Mutators.Teams.Application.Associates;

using Domain.Entities;
using ErrorOr;
using MediatR;
using Shared;

public class TrashAssociateCommand : IRequest<ErrorOr<bool>>, IUserTrackable
{
    public int Id { get; set; }
    public string? User { get; set; }
}