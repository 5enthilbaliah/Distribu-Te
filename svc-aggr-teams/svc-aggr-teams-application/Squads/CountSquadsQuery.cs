namespace DistribuTe.Aggregates.Teams.Application.Squads;

using Framework.AppEssentials.Implementations;
using MediatR;

public class CountSquadsQuery : IRequest<long>
{
    public LinqQueryFacade LinqQueryFacade { get; set; } = null!;
}