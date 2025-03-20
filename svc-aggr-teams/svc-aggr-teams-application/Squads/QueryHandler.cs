namespace DistribuTe.Aggregates.Teams.Application.Squads;

using DataContracts;
using Domain.Entities;
using Domain.Errors;
using ErrorOr;
using Framework.AppEssentials.Linq;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

public class QueryHandler(
    ITeamsReader<SquadAggregate, SquadId> reader,
    EntityLinqMapper<SquadAggregate, SquadId> baseMapper,
    EntityLinqMapper<SquadAssociateAggregate, SquadAssociateId> squadSubMapper,
    IMapper mapper) :
    IRequestHandler<YieldSquadsQuery, ErrorOr<IList<SquadModel>>>,
    IRequestHandler<PickSquadQuery, ErrorOr<SquadModel>>,
    IRequestHandler<CountSquadsQuery, long>
{
    private readonly ITeamsReader<SquadAggregate, SquadId> _reader =
        reader ?? throw new ArgumentNullException(nameof(reader));

    private readonly EntityLinqMapper<SquadAggregate, SquadId> _baseMapper =
        baseMapper ?? throw new ArgumentNullException(nameof(baseMapper));

    private readonly EntityLinqMapper<SquadAssociateAggregate, SquadAssociateId> _squadSubMapper =
        squadSubMapper ?? throw new ArgumentNullException(nameof(squadSubMapper));

    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    
    private Func<IQueryable<SquadAggregate>, IQueryable<SquadAggregate>> FindExpander(EntityLinqFacade facade)
    {
        return (queryable) =>
        {
            if (!facade.InnerWhereClauses.TryGetValue("squad_associates", out var clause))
                return queryable;
                
            var squadAssociateExpr = _squadSubMapper.MapAsSearchExpression(clause);
            if (squadAssociateExpr != null)
            {
                return queryable.Include(a => 
                    a.SquadAssociates.AsQueryable().Where(squadAssociateExpr));
            }
                
            return queryable.Include(a => a.SquadAssociates);
        };
    }

    
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

    public async Task<ErrorOr<SquadModel>> Handle(PickSquadQuery request, CancellationToken cancellationToken)
    {
        Func<IQueryable<SquadAggregate>, IQueryable<SquadAggregate>>? expander = null;
        if (request.EntityLinqFacade.InnerWhereClauses.Count != 0)
        {
            expander = FindExpander(request.EntityLinqFacade);
        }
        
        var entity = await _reader.PickAsync(new SquadId(request.Id), expander: expander,
            cancellationToken: cancellationToken);

        if (entity == null)
            return Errors.Squads.NotFound;
        
        return _mapper.Map<SquadModel>(entity);
    }

    public async Task<long> Handle(CountSquadsQuery request, CancellationToken cancellationToken)
    {
        var expression = _baseMapper.MapAsSearchExpression(request.EntityLinqFacade);
        return await _reader.CountAsync(expression, cancellationToken);
    }
}