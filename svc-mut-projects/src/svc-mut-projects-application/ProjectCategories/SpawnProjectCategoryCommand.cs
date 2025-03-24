namespace DistribuTe.Mutators.Projects.Application.ProjectCategories;

using DataContracts;
using ErrorOr;
using Framework.AppEssentials;
using MediatR;

public class SpawnProjectCategoryCommand : IRequest<ErrorOr<ProjectCategoryResponse>>, IUserTrackable
{
    public ProjectCategoryRequest ProjectCategory { get; set; } = null!;
    public string? User { get; set; }
}