namespace DistribuTe.Aggregates.Teams.Application.Squads;

using Base;
using DataContracts;
using Domain.Entities;
using ErrorOr;
using Framework.AppEssentials;
using Framework.AppEssentials.Linq;
using MapsterMapper;
using MediatR;

public class YieldSquadsQueryHandler(
    IAggregateReader<SquadAggregate, SquadId> reader,
    EntityLinqMapper<SquadAggregate, SquadId> baseMapper,
    EntityLinqMapper<SquadAssociateAggregate, SquadAssociateId> squadSubMapper,
    IMapper mapper) : SquadQueryHandler(squadSubMapper),
    IRequestHandler<YieldSquadsQuery, ErrorOr<IList<SquadModel>>>
{
    private readonly IAggregateReader<SquadAggregate, SquadId> _reader =
        reader ?? throw new ArgumentNullException(nameof(reader));

    private readonly EntityLinqMapper<SquadAggregate, SquadId> _baseMapper =
        baseMapper ?? throw new ArgumentNullException(nameof(baseMapper));

    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    
    public async Task<ErrorOr<IList<SquadModel>>> Handle(YieldSquadsQuery request, CancellationToken cancellationToken)
    {
        var expression = _baseMapper.MapAsSearchExpression(request.EntityLinqFacade);
        Func<IQueryable<SquadAggregate>, IQueryable<SquadAggregate>>? expander = null;
        Func<IQueryable<SquadAggregate>, IQueryable<SquadAggregate>>? sorter = null;
        
        if (request.EntityLinqFacade.InnerWhereClauses.Count != 0)
            expander = FindExpander(request.EntityLinqFacade);

        if (request.EntityLinqFacade.OrderByClause.Count != 0)
            sorter = queryable => _baseMapper.ApplySort(queryable, request.EntityLinqFacade.OrderByClause);
        
        var skip = request.EntityLinqFacade.Skip;
        var take = request.EntityLinqFacade.Top;
        var entities = await _reader.YieldAsync(expression, skip, take, 
            expander: expander, sorter: sorter,
            cancellationToken: cancellationToken);
        
        return _mapper.Map<List<SquadModel>>(entities);
    }
}