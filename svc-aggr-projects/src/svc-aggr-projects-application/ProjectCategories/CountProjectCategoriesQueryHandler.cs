namespace DistribuTe.Aggregates.Projects.Application.ProjectCategories;

using Domain.Entities;
using Framework.AppEssentials;
using Framework.AppEssentials.Linq;
using MediatR;

public class CountProjectCategoriesQueryHandler(IAggregateReader<ProjectCategoryAggregate, ProjectCategoryId> reader,
    EntityLinqMapper<ProjectCategoryAggregate, ProjectCategoryId> baseMapper) :
    IRequestHandler<CountProjectCategoriesQuery, long>
{
    private readonly IAggregateReader<ProjectCategoryAggregate, ProjectCategoryId> _reader =
        reader ?? throw new ArgumentNullException(nameof(reader));

    private readonly EntityLinqMapper<ProjectCategoryAggregate, ProjectCategoryId> _baseMapper =
        baseMapper ?? throw new ArgumentNullException(nameof(baseMapper));
    
    public async Task<long> Handle(CountProjectCategoriesQuery request, CancellationToken cancellationToken)
    {
        var expression = _baseMapper.MapAsSearchExpression(request.EntityLinqFacade);
        return await _reader.CountAsync(expression, cancellationToken);
    }
}