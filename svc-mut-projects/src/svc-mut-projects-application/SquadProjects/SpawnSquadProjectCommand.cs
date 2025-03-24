namespace DistribuTe.Mutators.Projects.Application.SquadProjects;

using DataContracts;
using ErrorOr;
using Framework.AppEssentials;
using MediatR;

public class SpawnSquadProjectCommand : IRequest<ErrorOr<SquadProjectResponse>>, ITokenTrackable
{
    public SquadProjectRequest SquadProject { get; set; } = null!;
    public string? User { get; set; }
    public string? Token { get; set; }
}