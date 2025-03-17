namespace DistribuTe.Aggregates.Teams.Application.Squads;

using DataContracts;
using ErrorOr;
using Framework.AppEssentials.Implementations;
using MediatR;

public class YieldSquadsQuery : IRequest<ErrorOr<IList<SquadModel>>>
{
    public LinqQueryFacade LinqQueryFacade { get; init; } = null!;
    public int Skip { get; set; } = 0;
    public int Take { get; set; } = 100;
}