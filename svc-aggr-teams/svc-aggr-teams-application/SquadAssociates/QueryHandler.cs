namespace DistribuTe.Aggregates.Teams.Application.SquadAssociates;

using DataContracts;
using Domain.Entities;
using Domain.Errors;
using ErrorOr;
using Framework.AppEssentials.Linq;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

public class QueryHandler(
    ITeamsReader<SquadAssociateAggregate, SquadAssociateId> reader,
    EntityLinqMapper<SquadAssociateAggregate, SquadAssociateId> baseMapper,
    IMapper mapper) :
    IRequestHandler<YieldSquadAssociatesQuery, ErrorOr<IList<SquadAssociateModel>>>,
    IRequestHandler<PickSquadAssociateQuery, ErrorOr<SquadAssociateModel>>,
    IRequestHandler<CountSquadAssociatesQuery, long>
{
    private readonly ITeamsReader<SquadAssociateAggregate, SquadAssociateId> _reader =
        reader ?? throw new ArgumentNullException(nameof(reader));

    private readonly EntityLinqMapper<SquadAssociateAggregate, SquadAssociateId> _baseMapper =
        baseMapper ?? throw new ArgumentNullException(nameof(baseMapper));

    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    
    private Func<IQueryable<SquadAssociateAggregate>, IQueryable<SquadAssociateAggregate>> FindExpander(EntityLinqFacade facade)
    {
        return (queryable) =>
        {
            if (facade.InnerWhereClauses.TryGetValue("squad", out _))
            {
                queryable = queryable.Include(a => a.Squad);
            }
            
            if (facade.InnerWhereClauses.TryGetValue("associate", out _))
            {
                queryable = queryable.Include(a => a.Associate);
            }
            
            return queryable;
        };
    }
    
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

    public async Task<ErrorOr<SquadAssociateModel>> Handle(PickSquadAssociateQuery request, CancellationToken cancellationToken)
    {
        Func<IQueryable<SquadAssociateAggregate>, IQueryable<SquadAssociateAggregate>>? expander = null;
        if (request.EntityLinqFacade.InnerWhereClauses.Count != 0)
        {
            expander = FindExpander(request.EntityLinqFacade);
        }
        
        var squadId = new SquadId(request.SquadId);
        var associateId = new AssociateId(request.AssociateId);
        var entity = await _reader.PickAsync(new SquadAssociateId(squadId, associateId), 
            expander: expander, cancellationToken: cancellationToken);

        if (entity == null)
            return Errors.SquadAssociates.NotFound;
        
        return _mapper.Map<SquadAssociateModel>(entity);
    }

    public async Task<long> Handle(CountSquadAssociatesQuery request, CancellationToken cancellationToken)
    {
        var expression = _baseMapper.MapAsSearchExpression(request.EntityLinqFacade);
        return await _reader.CountAsync(expression, cancellationToken);
    }
}