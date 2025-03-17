namespace DistribuTe.Aggregates.Teams.Application.SquadAssociates;

using DataContracts;
using Domain.Entities;
using Domain.Errors;
using ErrorOr;
using Framework.AppEssentials.Implementations;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

public class QueryHandler(
    ITeamsReader<SquadAssociateAggregate, SquadAssociateId> reader,
    LinqQueryFilterMapper<SquadAssociateAggregate, SquadAssociateId> baseMapper,
    IMapper mapper) :
    IRequestHandler<YieldSquadAssociatesQuery, ErrorOr<IList<SquadAssociateModel>>>,
    IRequestHandler<PickSquadAssociateQuery, ErrorOr<SquadAssociateModel>>
{
    private readonly ITeamsReader<SquadAssociateAggregate, SquadAssociateId> _reader =
        reader ?? throw new ArgumentNullException(nameof(reader));

    private readonly LinqQueryFilterMapper<SquadAssociateAggregate, SquadAssociateId> _baseMapper =
        baseMapper ?? throw new ArgumentNullException(nameof(baseMapper));

    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    
    private Func<IQueryable<SquadAssociateAggregate>, IQueryable<SquadAssociateAggregate>> FindExpander(LinqQueryFacade facade)
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
        var expression = _baseMapper.MapAsSearchExpression(request.LinqQueryFacade);
        Func<IQueryable<SquadAssociateAggregate>, IQueryable<SquadAssociateAggregate>>? expander = null;
        
        if (request.LinqQueryFacade.InnerWhereClauses.Count != 0)
        {
            expander = FindExpander(request.LinqQueryFacade);
        }
        
        var skip = request.LinqQueryFacade.Skip;
        var take = request.LinqQueryFacade.Top;
        var entities = await _reader.YieldAsync(expression, skip, take, expander: expander, 
            cancellationToken: cancellationToken);
        
        return _mapper.Map<List<SquadAssociateModel>>(entities);
    }

    public async Task<ErrorOr<SquadAssociateModel>> Handle(PickSquadAssociateQuery request, CancellationToken cancellationToken)
    {
        Func<IQueryable<SquadAssociateAggregate>, IQueryable<SquadAssociateAggregate>>? expander = null;
        if (request.LinqQueryFacade.InnerWhereClauses.Count != 0)
        {
            expander = FindExpander(request.LinqQueryFacade);
        }
        
        var squadId = new SquadId(request.SquadId);
        var associateId = new AssociateId(request.AssociateId);
        var entity = await _reader.PickAsync(new SquadAssociateId(squadId, associateId), 
            expander: expander, cancellationToken: cancellationToken);

        if (entity == null)
            return Errors.SquadAssociates.NotFound;
        
        return _mapper.Map<SquadAssociateModel>(entity);
    }
}