namespace DistribuTe.Aggregates.Projects.Application.SquadProjects;

using DataContracts;
using ErrorOr;
using Framework.AppEssentials.Linq;
using MediatR;

public class PickSquadProjectQuery : IRequest<ErrorOr<SquadProjectModel>>
{
    public int SquadId { get; set; }
    public int ProjectId { get; set; }
    public EntityLinqFacade EntityLinqFacade { get; init; } = null!;
}