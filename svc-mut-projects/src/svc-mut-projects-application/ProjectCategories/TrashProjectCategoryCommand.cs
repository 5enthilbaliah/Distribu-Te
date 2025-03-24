namespace DistribuTe.Mutators.Projects.Application.ProjectCategories;

using ErrorOr;
using Framework.AppEssentials;
using MediatR;

public class TrashProjectCategoryCommand : IRequest<ErrorOr<bool>>, IUserTrackable
{
    public int Id { get; set; }
    public string? User { get; set; }
}