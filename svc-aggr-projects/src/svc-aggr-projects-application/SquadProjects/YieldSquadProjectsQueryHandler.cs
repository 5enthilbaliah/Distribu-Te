namespace DistribuTe.Aggregates.Projects.Application.SquadProjects;

using _base;
using DataContracts;
using Domain.Entities;
using ErrorOr;
using Framework.AppEssentials;
using Framework.AppEssentials.Linq;
using MapsterMapper;
using MediatR;

public class YieldSquadProjectsQueryHandler(
    IAggregateReader<SquadProjectAggregate, SquadProjectId> reader,
    EntityLinqMapper<SquadProjectAggregate, SquadProjectId> baseMapper,
    IMapper mapper) : SquadProjectQueryHandler,
    IRequestHandler<YieldSquadProjectsQuery, ErrorOr<IList<SquadProjectModel>>>
{
    private readonly IAggregateReader<SquadProjectAggregate, SquadProjectId> _reader =
        reader ?? throw new ArgumentNullException(nameof(reader));

    private readonly EntityLinqMapper<SquadProjectAggregate, SquadProjectId> _baseMapper =
        baseMapper ?? throw new ArgumentNullException(nameof(baseMapper));

    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    
    public async Task<ErrorOr<IList<SquadProjectModel>>> Handle(YieldSquadProjectsQuery request, CancellationToken cancellationToken)
    {
        var expression = _baseMapper.MapAsSearchExpression(request.EntityLinqFacade);
        Func<IQueryable<SquadProjectAggregate>, IQueryable<SquadProjectAggregate>>? expander = null;
        Func<IQueryable<SquadProjectAggregate>, IQueryable<SquadProjectAggregate>>? sorter = null;
        
        if (request.EntityLinqFacade.InnerWhereClauses.Count != 0)
            expander = FindExpander(request.EntityLinqFacade);

        if (request.EntityLinqFacade.OrderByClause.Count != 0)
            sorter = queryable => _baseMapper.ApplySort(queryable, request.EntityLinqFacade.OrderByClause);
        
        var skip = request.EntityLinqFacade.Skip;
        var take = request.EntityLinqFacade.Top;
        var entities = await _reader.YieldAsync(expression, skip, take, 
            expander: expander, sorter: sorter,
            cancellationToken: cancellationToken);
        
        return _mapper.Map<List<SquadProjectModel>>(entities);
    }
}