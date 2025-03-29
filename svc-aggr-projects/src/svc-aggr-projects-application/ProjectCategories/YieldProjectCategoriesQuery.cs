namespace DistribuTe.Aggregates.Projects.Application.ProjectCategories;

using DataContracts;
using ErrorOr;
using Framework.AppEssentials.Linq;
using MediatR;

public class YieldProjectCategoriesQuery : IRequest<ErrorOr<IList<ProjectCategoryModel>>>
{
    public EntityLinqFacade EntityLinqFacade { get; init; } = null!;
    public int Skip { get; set; } = 0;
    public int Take { get; set; } = 100;
}