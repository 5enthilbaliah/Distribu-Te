namespace DistribuTe.Mutators.Projects.Application.ProjectCategories;

using DataContracts;
using ErrorOr;
using Framework.AppEssentials;
using MediatR;

public class CommitProjectCategoryCommand : IRequest<ErrorOr<ProjectCategoryResponse>>, IUserTrackable
{
    public int Id { get; set; }
    public ProjectCategoryRequest ProjectCategory { get; set; } = null!;
    public string? User { get; set; }
}