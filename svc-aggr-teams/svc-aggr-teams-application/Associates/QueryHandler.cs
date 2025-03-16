namespace DistribuTe.Aggregates.Teams.Application.Associates;

using DataContracts;
using Domain.Entities;
using ErrorOr;
using Framework.AppEssentials.Implementations;
using MapsterMapper;
using MediatR;

public class QueryHandler(ITeamsReader<AssociateAggregate, AssociateId> reader,
    WhereClauseMapper<AssociateAggregate, AssociateId> whereMapper, IMapper mapper) : 
    IRequestHandler<SearchAssociatesQuery, ErrorOr<IList<AssociateModel>>>
{
    private readonly ITeamsReader<AssociateAggregate, AssociateId> _reader = reader ?? throw new ArgumentNullException(nameof(reader));
    private readonly WhereClauseMapper<AssociateAggregate, AssociateId> _whereMapper = whereMapper ?? throw new ArgumentNullException(nameof(whereMapper));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    
    public async Task<ErrorOr<IList<AssociateModel>>> Handle(SearchAssociatesQuery request, CancellationToken cancellationToken)
    {
        var expression = _whereMapper.MapAsSearchExpression(request.WhereClauseFacade);
        var entities = await _reader.YieldAsync(expression, cancellationToken: cancellationToken);
        return _mapper.Map<List<AssociateModel>>(entities);
    }
}