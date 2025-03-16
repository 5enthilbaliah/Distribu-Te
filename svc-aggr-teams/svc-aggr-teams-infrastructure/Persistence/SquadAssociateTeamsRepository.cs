namespace DistribuTe.Aggregates.Teams.Infrastructure.Persistence;

using System.Diagnostics.CodeAnalysis;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

internal sealed class SquadAssociateTeamsRepository(TeamDatabaseContext context)
    : TeamsRepository<SquadAssociateAggregate, SquadAssociateId>(context)
{
    [ExcludeFromCodeCoverage]
    public override async Task<SquadAssociateAggregate?> PickAsync(SquadAssociateId id,
        Action<IQueryable<SquadAssociateAggregate>>? expander = null, CancellationToken cancellationToken = default)
    {
        return await DbContext.SquadAssociates
            .SingleOrDefaultAsync(x => x.SquadId == id.SquadId && x.AssociateId == id.AssociateId, 
                cancellationToken: cancellationToken);
    }
}