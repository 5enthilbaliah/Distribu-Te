namespace DistribuTe.Aggregates.Projects.Application.SquadProjects;

using Domain.Entities;
using Framework.AppEssentials;
using Framework.AppEssentials.Linq;
using MediatR;

public class CountSquadProjectsQueryHandler(IAggregateReader<SquadProjectAggregate, SquadProjectId> reader,
    EntityLinqMapper<SquadProjectAggregate, SquadProjectId> baseMapper) :
    IRequestHandler<CountSquadProjectsQuery, long>
{
    private readonly IAggregateReader<SquadProjectAggregate, SquadProjectId> _reader =
        reader ?? throw new ArgumentNullException(nameof(reader));

    private readonly EntityLinqMapper<SquadProjectAggregate, SquadProjectId> _baseMapper =
        baseMapper ?? throw new ArgumentNullException(nameof(baseMapper));
    
    public async Task<long> Handle(CountSquadProjectsQuery request, CancellationToken cancellationToken)
    {
        var expression = _baseMapper.MapAsSearchExpression(request.EntityLinqFacade);
        return await _reader.CountAsync(expression, cancellationToken);
    }
}