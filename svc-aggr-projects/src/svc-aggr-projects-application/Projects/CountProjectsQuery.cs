namespace DistribuTe.Aggregates.Projects.Application.Projects;

using Framework.AppEssentials.Linq;
using MediatR;

public class CountProjectsQuery : IRequest<long>
{
    public EntityLinqFacade EntityLinqFacade { get; set; } = null!;
}