namespace DistribuTe.Aggregates.Teams.Application.Associates;

using DataContracts;
using ErrorOr;
using Framework.AppEssentials.Linq;
using MediatR;

public class PickAssociateQuery : IRequest<ErrorOr<AssociateModel>>
{
    public int Id { get; set; }
    public LinqQueryFacade LinqQueryFacade { get; init; } = null!;
}