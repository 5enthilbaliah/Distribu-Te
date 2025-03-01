namespace DistribuTe.Mutators.Teams.Application.Associates;

using Domain.Entities;
using MediatR;

public class SpawnAssociateCommand : IRequest<AssociateId>
{
    public Associate Associate { get; set; } = null!;
}