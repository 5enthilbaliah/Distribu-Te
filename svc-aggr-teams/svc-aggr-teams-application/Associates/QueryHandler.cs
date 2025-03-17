namespace DistribuTe.Aggregates.Teams.Application.Associates;

using DataContracts;
using Domain.Entities;
using Domain.Errors;
using ErrorOr;
using Framework.AppEssentials.Implementations;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

public class QueryHandler(
    ITeamsReader<AssociateAggregate, AssociateId> reader,
    LinqQueryFilterMapper<AssociateAggregate, AssociateId> baseMapper,
    LinqQueryFilterMapper<SquadAssociateAggregate, SquadAssociateId> squadSubMapper,
    IMapper mapper) :
    IRequestHandler<YieldAssociatesQuery, ErrorOr<IList<AssociateModel>>>,
    IRequestHandler<PickAssociateQuery, ErrorOr<AssociateModel>>
{
    private readonly ITeamsReader<AssociateAggregate, AssociateId> _reader =
        reader ?? throw new ArgumentNullException(nameof(reader));

    private readonly LinqQueryFilterMapper<AssociateAggregate, AssociateId> _baseMapper =
        baseMapper ?? throw new ArgumentNullException(nameof(baseMapper));

    private readonly LinqQueryFilterMapper<SquadAssociateAggregate, SquadAssociateId> _squadSubMapper =
        squadSubMapper ?? throw new ArgumentNullException(nameof(squadSubMapper));

    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

    private Func<IQueryable<AssociateAggregate>, IQueryable<AssociateAggregate>> FindExpander(WhereClauseFacade facade)
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

    public async Task<ErrorOr<IList<AssociateModel>>> Handle(YieldAssociatesQuery request,
        CancellationToken cancellationToken)
    {
        var expression = _baseMapper.MapAsSearchExpression(request.WhereClauseFacade);
        Func<IQueryable<AssociateAggregate>, IQueryable<AssociateAggregate>>? expander = null;
        
        if (request.WhereClauseFacade.InnerWhereClauses.Count != 0)
        {
            expander = FindExpander(request.WhereClauseFacade);
        }
        
        var entities = await _reader.YieldAsync(expression, expander: expander, 
            cancellationToken: cancellationToken);
        
        return _mapper.Map<List<AssociateModel>>(entities);
    }

    public async Task<ErrorOr<AssociateModel>> Handle(PickAssociateQuery request, CancellationToken cancellationToken)
    {
        Func<IQueryable<AssociateAggregate>, IQueryable<AssociateAggregate>>? expander = null;
        if (request.WhereClauseFacade.InnerWhereClauses.Count != 0)
        {
            expander = FindExpander(request.WhereClauseFacade);
        }
        
        var entity = await _reader.PickAsync(new AssociateId(request.Id), expander: expander,
            cancellationToken: cancellationToken);

        if (entity == null)
            return Errors.Associates.NotFound;
        
        return _mapper.Map<AssociateModel>(entity);
    }
}