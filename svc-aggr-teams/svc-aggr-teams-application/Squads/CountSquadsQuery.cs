namespace DistribuTe.Aggregates.Teams.Application.Squads;

using Framework.AppEssentials.Linq;
using MediatR;

public class CountSquadsQuery : IRequest<long>
{
    public EntityLinqFacade EntityLinqFacade { get; set; } = null!;
}