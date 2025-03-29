namespace DistribuTe.Aggregates.Projects.Application.Projects;

using _base;
using DataContracts;
using Domain.Entities;
using ErrorOr;
using Framework.AppEssentials;
using Framework.AppEssentials.Linq;
using MapsterMapper;
using MediatR;

public class YieldProjectsQueryHandler(
    IAggregateReader<ProjectAggregate, ProjectId> reader,
    EntityLinqMapper<ProjectAggregate, ProjectId> baseMapper,
    EntityLinqMapper<SquadProjectAggregate, SquadProjectId> squadSubMapper,
    IMapper mapper) : ProjectQueryHandler(squadSubMapper),
    IRequestHandler<YieldProjectsQuery, ErrorOr<IList<ProjectModel>>>
{
    private readonly IAggregateReader<ProjectAggregate, ProjectId> _reader =
        reader ?? throw new ArgumentNullException(nameof(reader));

    private readonly EntityLinqMapper<ProjectAggregate, ProjectId> _baseMapper =
        baseMapper ?? throw new ArgumentNullException(nameof(baseMapper));

    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    
    public async Task<ErrorOr<IList<ProjectModel>>> Handle(YieldProjectsQuery request, CancellationToken cancellationToken)
    {
        var expression = _baseMapper.MapAsSearchExpression(request.EntityLinqFacade);
        Func<IQueryable<ProjectAggregate>, IQueryable<ProjectAggregate>>? expander = null;
        Func<IQueryable<ProjectAggregate>, IQueryable<ProjectAggregate>>? sorter = null;
        
        if (request.EntityLinqFacade.InnerWhereClauses.Count != 0)
            expander = FindExpander(request.EntityLinqFacade);

        if (request.EntityLinqFacade.OrderByClause.Count != 0)
            sorter = queryable => _baseMapper.ApplySort(queryable, request.EntityLinqFacade.OrderByClause);
        
        var skip = request.EntityLinqFacade.Skip;
        var take = request.EntityLinqFacade.Top;
        var entities = await _reader.YieldAsync(expression, skip, take, 
            expander: expander, sorter: sorter,
            cancellationToken: cancellationToken);
        
        return _mapper.Map<List<ProjectModel>>(entities);
    }
}