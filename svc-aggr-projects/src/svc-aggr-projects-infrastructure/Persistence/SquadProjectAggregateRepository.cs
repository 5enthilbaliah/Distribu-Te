namespace DistribuTe.Aggregates.Projects.Infrastructure.Persistence;

using System.Diagnostics.CodeAnalysis;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

internal sealed class SquadProjectAggregateRepository(ProjectSchemaDatabaseContext context)
    : AggregateRepository<SquadProjectAggregate, SquadProjectId>(context)
{
    [ExcludeFromCodeCoverage]
    public override async Task<SquadProjectAggregate?> PickAsync(SquadProjectId id,
        Func<IQueryable<SquadProjectAggregate>, IQueryable<SquadProjectAggregate>>? expander = null, CancellationToken cancellationToken = default)
    {
        var queryable = DbContext.Set<SquadProjectAggregate>().AsQueryable();
        if (expander != null)
            queryable = expander(queryable);
        
        queryable = queryable.AsNoTracking();
        
        // var y = queryable.OrderByDescending(x => x.Id)
        
        return await queryable
            .SingleOrDefaultAsync(x => x.SquadId == id.SquadId && x.ProjectId == id.ProjectId, 
                cancellationToken: cancellationToken);
    }
}