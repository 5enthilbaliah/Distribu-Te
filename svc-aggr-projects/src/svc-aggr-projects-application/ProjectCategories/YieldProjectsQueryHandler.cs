namespace DistribuTe.Aggregates.Projects.Application.ProjectCategories;

using _base;
using DataContracts;
using Domain.Entities;
using ErrorOr;
using Framework.AppEssentials;
using Framework.AppEssentials.Linq;
using MapsterMapper;
using MediatR;

public class YieldProjectsQueryHandler(
    IAggregateReader<ProjectCategoryAggregate, ProjectCategoryId> reader,
    EntityLinqMapper<ProjectCategoryAggregate, ProjectCategoryId> baseMapper,
    EntityLinqMapper<ProjectAggregate, ProjectId> squadSubMapper,
    IMapper mapper) : ProjectCategoryQueryHandler(squadSubMapper),
    IRequestHandler<YieldProjectCategoriesQuery, ErrorOr<IList<ProjectCategoryModel>>>
{
    private readonly IAggregateReader<ProjectCategoryAggregate, ProjectCategoryId> _reader =
        reader ?? throw new ArgumentNullException(nameof(reader));

    private readonly EntityLinqMapper<ProjectCategoryAggregate, ProjectCategoryId> _baseMapper =
        baseMapper ?? throw new ArgumentNullException(nameof(baseMapper));

    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    
    public async Task<ErrorOr<IList<ProjectCategoryModel>>> Handle(YieldProjectCategoriesQuery request, CancellationToken cancellationToken)
    {
        var expression = _baseMapper.MapAsSearchExpression(request.EntityLinqFacade);
        Func<IQueryable<ProjectCategoryAggregate>, IQueryable<ProjectCategoryAggregate>>? expander = null;
        Func<IQueryable<ProjectCategoryAggregate>, IQueryable<ProjectCategoryAggregate>>? sorter = null;
        
        if (request.EntityLinqFacade.InnerWhereClauses.Count != 0)
            expander = FindExpander(request.EntityLinqFacade);

        if (request.EntityLinqFacade.OrderByClause.Count != 0)
            sorter = queryable => _baseMapper.ApplySort(queryable, request.EntityLinqFacade.OrderByClause);
        
        var skip = request.EntityLinqFacade.Skip;
        var take = request.EntityLinqFacade.Top;
        var entities = await _reader.YieldAsync(expression, skip, take, 
            expander: expander, sorter: sorter,
            cancellationToken: cancellationToken);
        
        return _mapper.Map<List<ProjectCategoryModel>>(entities);
    }
}