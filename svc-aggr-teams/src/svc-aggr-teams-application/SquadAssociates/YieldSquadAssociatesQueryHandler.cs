namespace DistribuTe.Aggregates.Teams.Application.SquadAssociates;

using Base;
using DataContracts;
using Domain.Entities;
using ErrorOr;
using Framework.AppEssentials;
using Framework.AppEssentials.Linq;
using MapsterMapper;
using MediatR;

public class YieldSquadAssociatesQueryHandler(
    IAggregateReader<SquadAssociateAggregate, SquadAssociateId> reader,
    EntityLinqMapper<SquadAssociateAggregate, SquadAssociateId> baseMapper,
    IMapper mapper) : SquadAssociateQueryHandler,
    IRequestHandler<YieldSquadAssociatesQuery, ErrorOr<IList<SquadAssociateModel>>>
{
    private readonly IAggregateReader<SquadAssociateAggregate, SquadAssociateId> _reader =
        reader ?? throw new ArgumentNullException(nameof(reader));

    private readonly EntityLinqMapper<SquadAssociateAggregate, SquadAssociateId> _baseMapper =
        baseMapper ?? throw new ArgumentNullException(nameof(baseMapper));

    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    
    public async Task<ErrorOr<IList<SquadAssociateModel>>> Handle(YieldSquadAssociatesQuery request, CancellationToken cancellationToken)
    {
        var expression = _baseMapper.MapAsSearchExpression(request.EntityLinqFacade);
        Func<IQueryable<SquadAssociateAggregate>, IQueryable<SquadAssociateAggregate>>? expander = null;
        Func<IQueryable<SquadAssociateAggregate>, IQueryable<SquadAssociateAggregate>>? sorter = null;
        
        if (request.EntityLinqFacade.InnerWhereClauses.Count != 0)
            expander = FindExpander(request.EntityLinqFacade);

        if (request.EntityLinqFacade.OrderByClause.Count != 0)
            sorter = queryable => _baseMapper.ApplySort(queryable, request.EntityLinqFacade.OrderByClause);
        
        var skip = request.EntityLinqFacade.Skip;
        var take = request.EntityLinqFacade.Top;
        var entities = await _reader.YieldAsync(expression, skip, take, 
            expander: expander, sorter: sorter,
            cancellationToken: cancellationToken);
        
        return _mapper.Map<List<SquadAssociateModel>>(entities);
    }
}