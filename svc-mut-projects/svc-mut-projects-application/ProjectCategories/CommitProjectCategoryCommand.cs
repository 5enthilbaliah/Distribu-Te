namespace DistribuTe.Mutators.Projects.Application.ProjectCategories;

using DataContracts;
using ErrorOr;
using MediatR;
using Shared;

public class CommitProjectCategoryCommand : IRequest<ErrorOr<ProjectCategoryResponse>>, IUserTrackable
{
    public int Id { get; set; }
    public ProjectCategoryRequest ProjectCategory { get; set; } = null!;
    public string? User { get; set; }
}