namespace DistribuTe.Aggregates.Teams.Application.Associates;

using DataContracts;
using ErrorOr;
using Framework.AppEssentials.Implementations;
using MediatR;

public class PickAssociateQuery : IRequest<ErrorOr<AssociateModel>>
{
    public int Id { get; set; }
    public WhereClauseFacade WhereClauseFacade { get; init; } = null!;
}