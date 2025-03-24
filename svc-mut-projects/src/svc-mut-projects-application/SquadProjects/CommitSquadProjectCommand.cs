namespace DistribuTe.Mutators.Projects.Application.SquadProjects;

using DataContracts;
using ErrorOr;
using Framework.AppEssentials;
using MediatR;

public class CommitSquadProjectCommand : IRequest<ErrorOr<SquadProjectResponse>>, IUserTrackable
{
    public int SquadId { get; set; }
    public int ProjectId { get; set; }
    public SquadProjectRequest SquadProject { get; set; } = null!;
    public string? User { get; set; }
}