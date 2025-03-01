namespace DistribuTe.Mutators.Teams.Application.Associates;

using Domain.Entities;
using MediatR;

public class TrashAssociateCommand : IRequest<bool>
{
    public int Id { get; set; }
}