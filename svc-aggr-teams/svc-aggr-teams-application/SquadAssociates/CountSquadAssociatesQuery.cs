namespace DistribuTe.Aggregates.Teams.Application.SquadAssociates;

using Framework.AppEssentials.Implementations;
using MediatR;

public class CountSquadAssociatesQuery : IRequest<long>
{
    public LinqQueryFacade LinqQueryFacade { get; set; } = null!;
}