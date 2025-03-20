namespace DistribuTe.Aggregates.Teams.Application.Associates;

using DataContracts;
using Domain.Entities;
using Domain.Errors;
using ErrorOr;
using Framework.AppEssentials.Linq;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

public class QueryHandler(
    ITeamsReader<AssociateAggregate, AssociateId> reader,
    EntityLinqMapper<AssociateAggregate, AssociateId> baseMapper,
    EntityLinqMapper<SquadAssociateAggregate, SquadAssociateId> squadSubMapper,
    IMapper mapper) :
    IRequestHandler<YieldAssociatesQuery, ErrorOr<IList<AssociateModel>>>,
    IRequestHandler<PickAssociateQuery, ErrorOr<AssociateModel>>,
    IRequestHandler<CountAssociatesQuery, long>
{
    private readonly ITeamsReader<AssociateAggregate, AssociateId> _reader =
        reader ?? throw new ArgumentNullException(nameof(reader));

    private readonly EntityLinqMapper<AssociateAggregate, AssociateId> _baseMapper =
        baseMapper ?? throw new ArgumentNullException(nameof(baseMapper));

    private readonly EntityLinqMapper<SquadAssociateAggregate, SquadAssociateId> _squadSubMapper =
        squadSubMapper ?? throw new ArgumentNullException(nameof(squadSubMapper));

    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

    private Func<IQueryable<AssociateAggregate>, IQueryable<AssociateAggregate>> FindExpander(EntityLinqFacade facade)
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
        var expression = _baseMapper.MapAsSearchExpression(request.EntityLinqFacade);
        Func<IQueryable<AssociateAggregate>, IQueryable<AssociateAggregate>>? expander = null;
        Func<IQueryable<AssociateAggregate>, IQueryable<AssociateAggregate>>? sorter = null;
        
        if (request.EntityLinqFacade.InnerWhereClauses.Count != 0)
            expander = FindExpander(request.EntityLinqFacade);

        if (request.EntityLinqFacade.OrderByClause.Count != 0)
            sorter = queryable => _baseMapper.ApplySort(queryable, request.EntityLinqFacade.OrderByClause);

        var skip = request.EntityLinqFacade.Skip;
        var take = request.EntityLinqFacade.Top;
        var entities = await _reader.YieldAsync(expression, skip, take, 
            expander: expander, sorter: sorter,
            cancellationToken: cancellationToken);
        
        return _mapper.Map<List<AssociateModel>>(entities);
    }

    public async Task<ErrorOr<AssociateModel>> Handle(PickAssociateQuery request, CancellationToken cancellationToken)
    {
        Func<IQueryable<AssociateAggregate>, IQueryable<AssociateAggregate>>? expander = null;
        if (request.EntityLinqFacade.InnerWhereClauses.Count != 0)
        {
            expander = FindExpander(request.EntityLinqFacade);
        }
        
        var entity = await _reader.PickAsync(new AssociateId(request.Id), expander: expander,
            cancellationToken: cancellationToken);

        if (entity == null)
            return Errors.Associates.NotFound;
        
        return _mapper.Map<AssociateModel>(entity);
    }

    public async Task<long> Handle(CountAssociatesQuery request, CancellationToken cancellationToken)
    {
        var expression = _baseMapper.MapAsSearchExpression(request.EntityLinqFacade);
        return await _reader.CountAsync(expression, cancellationToken);
    }
}