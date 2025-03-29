namespace DistribuTe.Aggregates.Projects.Application.SquadProjects;

using DataContracts;
using ErrorOr;
using Framework.AppEssentials.Linq;
using MediatR;

public class YieldSquadProjectsQuery : IRequest<ErrorOr<IList<SquadProjectModel>>>
{
    public EntityLinqFacade EntityLinqFacade { get; init; } = null!;
    public int Skip { get; set; } = 0;
    public int Take { get; set; } = 100;
}