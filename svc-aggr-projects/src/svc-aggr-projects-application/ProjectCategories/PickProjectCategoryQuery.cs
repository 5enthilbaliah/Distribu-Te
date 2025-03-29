namespace DistribuTe.Aggregates.Projects.Application.ProjectCategories;

using DataContracts;
using ErrorOr;
using Framework.AppEssentials.Linq;
using MediatR;

public class PickProjectCategoryQuery : IRequest<ErrorOr<ProjectCategoryModel>>
{
    public int Id { get; set; }
    public EntityLinqFacade EntityLinqFacade { get; init; } = null!;
}