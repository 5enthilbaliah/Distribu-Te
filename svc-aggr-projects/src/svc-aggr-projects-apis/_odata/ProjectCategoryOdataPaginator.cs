// ReSharper disable once CheckNamespace
namespace DistribuTe.Aggregates.Projects.Apis.Odata;

using Application.ProjectCategories;
using Framework.ApiEssentials.Odata;
using Framework.AppEssentials;
using Framework.AppEssentials.Linq;
using MediatR;

public class ProjectCategoryOdataPaginator(ISender sender, IRequestContext requestContext) : IOdataPaginator
{
    private readonly ISender _sender = sender ?? throw new ArgumentNullException(nameof(sender));
    private readonly IRequestContext _requestContext = requestContext ?? 
                                                       throw new ArgumentNullException(nameof(requestContext));

    public string Name => "aggregates_project_categories";
    
    public async Task<long> CountAsync(CancellationToken cancellationToken = default)
    {
        return await _sender.Send(new CountProjectCategoriesQuery
        {
            EntityLinqFacade = _requestContext.GetFeature<EntityLinqFacade>()!,
        }, cancellationToken);
    }
}