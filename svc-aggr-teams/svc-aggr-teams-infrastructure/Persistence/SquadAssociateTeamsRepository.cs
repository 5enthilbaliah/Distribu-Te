namespace DistribuTe.Aggregates.Teams.Infrastructure.Persistence;

using System.Diagnostics.CodeAnalysis;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

internal sealed class SquadAssociateTeamsRepository(TeamDatabaseContext context)
    : TeamsRepository<SquadAssociateAggregate, SquadAssociateId>(context)
{
    [ExcludeFromCodeCoverage]
    public override async Task<SquadAssociateAggregate?> PickAsync(SquadAssociateId id,
        Func<IQueryable<SquadAssociateAggregate>, IQueryable<SquadAssociateAggregate>>? expander = null, CancellationToken cancellationToken = default)
    {
        var queryable = DbContext.Set<SquadAssociateAggregate>().AsQueryable();
        if (expander != null)
            queryable = expander(queryable);
        
        queryable = queryable.AsNoTracking();
        
       // var y = queryable.OrderByDescending(x => x.Id)
        
        return await queryable
            .SingleOrDefaultAsync(x => x.SquadId == id.SquadId && x.AssociateId == id.AssociateId, 
                cancellationToken: cancellationToken);
    }
}