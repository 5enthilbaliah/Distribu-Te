namespace DistribuTe.Aggregates.Teams.Application.Squads;

using Framework.AppEssentials.Linq;
using MediatR;

public class CountSquadsQuery : IRequest<long>
{
    public LinqQueryFacade LinqQueryFacade { get; set; } = null!;
}