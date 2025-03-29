namespace DistribuTe.Aggregates.Projects.Application.Projects;

using Domain.Entities;
using Framework.AppEssentials;
using Framework.AppEssentials.Linq;
using MediatR;

public class CountSquadsQueryHandler(IAggregateReader<ProjectAggregate, ProjectId> reader,
    EntityLinqMapper<ProjectAggregate, ProjectId> baseMapper) :
    IRequestHandler<CountProjectsQuery, long>
{
    private readonly IAggregateReader<ProjectAggregate, ProjectId> _reader =
        reader ?? throw new ArgumentNullException(nameof(reader));

    private readonly EntityLinqMapper<ProjectAggregate, ProjectId> _baseMapper =
        baseMapper ?? throw new ArgumentNullException(nameof(baseMapper));
    
    public async Task<long> Handle(CountProjectsQuery request, CancellationToken cancellationToken)
    {
        var expression = _baseMapper.MapAsSearchExpression(request.EntityLinqFacade);
        return await _reader.CountAsync(expression, cancellationToken);
    }
}