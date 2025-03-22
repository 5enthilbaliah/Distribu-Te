namespace DistribuTe.Mutators.Projects.Application.SquadProjects;

using ErrorOr;
using MediatR;

public class TrashSquadProjectCommand : IRequest<ErrorOr<bool>>
{
    public int SquadId { get; set; }
    public int ProjectId { get; set; }
}