namespace DistribuTe.Aggregates.Teams.Application.Associates;

using Domain.Entities;
using Framework.AppEssentials;
using Framework.AppEssentials.Linq;
using MediatR;

public class CountAssociatesQueryHandler(IAggregateReader<AssociateAggregate, AssociateId> reader,
    EntityLinqMapper<AssociateAggregate, AssociateId> baseMapper) :
    IRequestHandler<CountAssociatesQuery, long>
{
    private readonly IAggregateReader<AssociateAggregate, AssociateId> _reader =
        reader ?? throw new ArgumentNullException(nameof(reader));

    private readonly EntityLinqMapper<AssociateAggregate, AssociateId> _baseMapper =
        baseMapper ?? throw new ArgumentNullException(nameof(baseMapper));
    
    public async Task<long> Handle(CountAssociatesQuery request, CancellationToken cancellationToken)
    {
        var expression = _baseMapper.MapAsSearchExpression(request.EntityLinqFacade);
        return await _reader.CountAsync(expression, cancellationToken);
    }
}