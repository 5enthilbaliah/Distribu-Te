namespace DistribuTe.Mutators.Projects.Application.Projects;

using DataContracts;
using ErrorOr;
using Framework.AppEssentials;
using MediatR;

public class SpawnProjectCommand : IRequest<ErrorOr<ProjectResponse>>, IUserTrackable
{
    public ProjectRequest Project { get; set; } = null!;
    public string? User { get; set; }
}