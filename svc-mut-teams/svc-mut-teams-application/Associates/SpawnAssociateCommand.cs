namespace DistribuTe.Mutators.Teams.Application.Associates;

using Domain.Entities;
using MediatR;
using Models;

public class SpawnAssociateCommand : IRequest<AssociateVm>
{
    public AssociateRm Associate { get; set; } = null!;
}