namespace DistribuTe.Aggregates.Projects.Application.SquadProjects;

using Framework.AppEssentials.Linq;
using MediatR;

public class CountSquadProjectsQuery : IRequest<long>
{
    public EntityLinqFacade EntityLinqFacade { get; set; } = null!;
}