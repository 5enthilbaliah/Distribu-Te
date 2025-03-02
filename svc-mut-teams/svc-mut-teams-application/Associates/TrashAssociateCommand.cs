namespace DistribuTe.Mutators.Teams.Application.Associates;

using Domain.Entities;
using MediatR;
using Shared;

public class TrashAssociateCommand : IRequest<bool>, IUserTrackable
{
    public int Id { get; set; }
    public string? User { get; set; }
}