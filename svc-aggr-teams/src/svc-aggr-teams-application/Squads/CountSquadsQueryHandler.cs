namespace DistribuTe.Aggregates.Teams.Application.Squads;

using Domain.Entities;
using Framework.AppEssentials;
using Framework.AppEssentials.Linq;
using MediatR;

public class CountSquadsQueryHandler(IAggregateReader<SquadAggregate, SquadId> reader,
    EntityLinqMapper<SquadAggregate, SquadId> baseMapper) :
    IRequestHandler<CountSquadsQuery, long>
{
    private readonly IAggregateReader<SquadAggregate, SquadId> _reader =
        reader ?? throw new ArgumentNullException(nameof(reader));

    private readonly EntityLinqMapper<SquadAggregate, SquadId> _baseMapper =
        baseMapper ?? throw new ArgumentNullException(nameof(baseMapper));
    
    public async Task<long> Handle(CountSquadsQuery request, CancellationToken cancellationToken)
    {
        var expression = _baseMapper.MapAsSearchExpression(request.EntityLinqFacade);
        return await _reader.CountAsync(expression, cancellationToken);
    }
}