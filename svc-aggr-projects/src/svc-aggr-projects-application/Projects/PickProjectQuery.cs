namespace DistribuTe.Aggregates.Projects.Application.Projects;

using DataContracts;
using ErrorOr;
using Framework.AppEssentials.Linq;
using MediatR;

public class PickProjectQuery : IRequest<ErrorOr<ProjectModel>>
{
    public int Id { get; set; }
    public EntityLinqFacade EntityLinqFacade { get; init; } = null!;
}