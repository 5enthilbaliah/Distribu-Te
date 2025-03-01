namespace DistribuTe.Mutators.Teams.Application.Associates;

using Domain.Entities;
using MediatR;
using Models;

public class CommitAssociateCommand : IRequest<AssociateVm>
{
    public int Id { get; set; }
    public AssociateRm Associate { get; set; } = null!;
}