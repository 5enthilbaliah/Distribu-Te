namespace DistribuTe.Aggregates.Teams.Application.Associates;

using Base;
using DataContracts;
using Domain.Entities;
using ErrorOr;
using Framework.AppEssentials.Linq;
using MapsterMapper;
using MediatR;

public class YieldAssociatesAssociateQueryHandler(
    ITeamsReader<AssociateAggregate, AssociateId> reader,
    EntityLinqMapper<AssociateAggregate, AssociateId> baseMapper,
    EntityLinqMapper<SquadAssociateAggregate, SquadAssociateId> squadSubMapper,
    IMapper mapper) : AssociateQueryHandler(squadSubMapper),
    IRequestHandler<YieldAssociatesQuery, ErrorOr<IList<AssociateModel>>>
{
    private readonly ITeamsReader<AssociateAggregate, AssociateId> _reader =
        reader ?? throw new ArgumentNullException(nameof(reader));

    private readonly EntityLinqMapper<AssociateAggregate, AssociateId> _baseMapper =
        baseMapper ?? throw new ArgumentNullException(nameof(baseMapper));
    
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

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
}