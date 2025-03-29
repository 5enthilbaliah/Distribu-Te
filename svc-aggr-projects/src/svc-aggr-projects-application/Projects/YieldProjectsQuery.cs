namespace DistribuTe.Aggregates.Projects.Application.Projects;

using DataContracts;
using ErrorOr;
using Framework.AppEssentials.Linq;
using MediatR;

public class YieldProjectsQuery : IRequest<ErrorOr<IList<ProjectModel>>>
{
    public EntityLinqFacade EntityLinqFacade { get; init; } = null!;
    public int Skip { get; set; } = 0;
    public int Take { get; set; } = 100;
}