namespace DistribuTe.Aggregates.Teams.Application.Squads;

using DataContracts;
using Domain.Entities;
using Domain.Errors;
using ErrorOr;
using Framework.AppEssentials.Implementations;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

public class QueryHandler(
    ITeamsReader<SquadAggregate, SquadId> reader,
    LinqQueryFilterMapper<SquadAggregate, SquadId> baseMapper,
    LinqQueryFilterMapper<SquadAssociateAggregate, SquadAssociateId> squadSubMapper,
    IMapper mapper) :
    IRequestHandler<YieldSquadsQuery, ErrorOr<IList<SquadModel>>>,
    IRequestHandler<PickSquadQuery, ErrorOr<SquadModel>>
{
    private readonly ITeamsReader<SquadAggregate, SquadId> _reader =
        reader ?? throw new ArgumentNullException(nameof(reader));

    private readonly LinqQueryFilterMapper<SquadAggregate, SquadId> _baseMapper =
        baseMapper ?? throw new ArgumentNullException(nameof(baseMapper));

    private readonly LinqQueryFilterMapper<SquadAssociateAggregate, SquadAssociateId> _squadSubMapper =
        squadSubMapper ?? throw new ArgumentNullException(nameof(squadSubMapper));

    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    
    private Func<IQueryable<SquadAggregate>, IQueryable<SquadAggregate>> FindExpander(WhereClauseFacade facade)
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
        var expression = _baseMapper.MapAsSearchExpression(request.WhereClauseFacade);
        Func<IQueryable<SquadAggregate>, IQueryable<SquadAggregate>>? expander = null;
        
        if (request.WhereClauseFacade.InnerWhereClauses.Count != 0)
        {
            expander = FindExpander(request.WhereClauseFacade);
        }
        
        var entities = await _reader.YieldAsync(expression, expander: expander, 
            cancellationToken: cancellationToken);
        
        return _mapper.Map<List<SquadModel>>(entities);
    }

    public async Task<ErrorOr<SquadModel>> Handle(PickSquadQuery request, CancellationToken cancellationToken)
    {
        Func<IQueryable<SquadAggregate>, IQueryable<SquadAggregate>>? expander = null;
        if (request.WhereClauseFacade.InnerWhereClauses.Count != 0)
        {
            expander = FindExpander(request.WhereClauseFacade);
        }
        
        var entity = await _reader.PickAsync(new SquadId(request.Id), expander: expander,
            cancellationToken: cancellationToken);

        if (entity == null)
            return Errors.Squads.NotFound;
        
        return _mapper.Map<SquadModel>(entity);
    }
}