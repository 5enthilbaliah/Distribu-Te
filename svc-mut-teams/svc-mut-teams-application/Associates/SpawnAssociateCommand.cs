namespace DistribuTe.Mutators.Teams.Application.Associates;

using Domain.Entities;
using MediatR;
using Models;
using Shared;

public class SpawnAssociateCommand : IRequest<AssociateVm>, IUserTrackable
{
    public AssociateRm Associate { get; set; } = null!;
    public string? User { get; set; }
}