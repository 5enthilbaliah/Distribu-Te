namespace DistribuTe.Mutators.Teams.Application.Associates;

using Domain.Entities;
using MediatR;

public class CommitAssociateCommand : IRequest<bool>
{
    public AssociateId Id { get; set; }
    public Associate Associate { get; set; } = null!;
}