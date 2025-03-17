namespace DistribuTe.Aggregates.Teams.Application.Associates;

using DataContracts;
using ErrorOr;
using Framework.AppEssentials.Implementations;
using MediatR;

public class YieldAssociatesQuery : IRequest<ErrorOr<IList<AssociateModel>>>
{
    public WhereClauseFacade WhereClauseFacade { get; set; }
    public int Skip { get; set; } = 0;
    public int Take { get; set; } = 100;
}

public class PickAssociateQuery : IRequest<ErrorOr<AssociateModel>>
{
    public int Id { get; set; }
    public WhereClauseFacade WhereClauseFacade { get; set; }
}