namespace DistribuTe.Mutators.Projects.Application.SquadProjects;

using DataContracts;
using ErrorOr;
using MediatR;
using Shared;

public class SpawnSquadProjectCommand : IRequest<ErrorOr<SquadProjectResponse>>, IUserTrackable
{
    public SquadProjectRequest SquadProject { get; set; } = null!;
    public string? User { get; set; }
}