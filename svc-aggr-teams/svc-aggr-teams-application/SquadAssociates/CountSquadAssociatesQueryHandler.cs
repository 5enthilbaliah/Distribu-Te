namespace DistribuTe.Aggregates.Teams.Application.SquadAssociates;

using Domain.Entities;
using Framework.AppEssentials.Linq;
using MediatR;

public class CountSquadAssociatesQueryHandler(ITeamsReader<SquadAssociateAggregate, SquadAssociateId> reader,
    EntityLinqMapper<SquadAssociateAggregate, SquadAssociateId> baseMapper) :
    IRequestHandler<CountSquadAssociatesQuery, long>
{
    private readonly ITeamsReader<SquadAssociateAggregate, SquadAssociateId> _reader =
        reader ?? throw new ArgumentNullException(nameof(reader));
    
    private readonly EntityLinqMapper<SquadAssociateAggregate, SquadAssociateId> _baseMapper =
        baseMapper ?? throw new ArgumentNullException(nameof(baseMapper));
    
    public async Task<long> Handle(CountSquadAssociatesQuery request, CancellationToken cancellationToken)
    {
        var expression = _baseMapper.MapAsSearchExpression(request.EntityLinqFacade);
        return await _reader.CountAsync(expression, cancellationToken);
    }
}