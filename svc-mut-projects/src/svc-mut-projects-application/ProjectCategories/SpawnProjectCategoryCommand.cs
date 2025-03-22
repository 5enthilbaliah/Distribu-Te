namespace DistribuTe.Mutators.Projects.Application.ProjectCategories;

using DataContracts;
using ErrorOr;
using MediatR;
using Shared;

public class SpawnProjectCategoryCommand : IRequest<ErrorOr<ProjectCategoryResponse>>, IUserTrackable
{
    public ProjectCategoryRequest ProjectCategory { get; set; } = null!;
    public string? User { get; set; }
}