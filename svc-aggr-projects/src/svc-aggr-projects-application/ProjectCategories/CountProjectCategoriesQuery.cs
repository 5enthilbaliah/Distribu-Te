namespace DistribuTe.Aggregates.Projects.Application.ProjectCategories;

using Framework.AppEssentials.Linq;
using MediatR;

public class CountProjectCategoriesQuery : IRequest<long>
{
    public EntityLinqFacade EntityLinqFacade { get; set; } = null!;
}