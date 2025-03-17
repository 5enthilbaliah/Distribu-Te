namespace DistribuTe.Aggregates.Teams.Application.SquadAssociates;

using DataContracts;
using ErrorOr;
using Framework.AppEssentials.Implementations;
using MediatR;

public class YieldSquadAssociatesQuery : IRequest<ErrorOr<IList<SquadAssociateModel>>>
{
    public WhereClauseFacade WhereClauseFacade { get; init; } = null!;
    public int Skip { get; set; } = 0;
    public int Take { get; set; } = 100;
}