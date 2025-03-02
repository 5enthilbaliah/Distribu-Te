namespace DistribuTe.Mutators.Teams.Infrastructure.Persistence;

using Domain.Entities;
using Microsoft.EntityFrameworkCore;

internal sealed class SquadAssociateTeamsRepository(TeamDatabaseContext context)
    : TeamsRepository<SquadAssociate, SquadAssociateId>(context)
{
    public override async Task CommitOneAsync(SquadAssociate mutation, Func<SquadAssociate, SquadAssociate> adapter, 
        CancellationToken cancellationToken = default)
    {
        var entity = await DbContext.SquadAssociates
            .SingleOrDefaultAsync(x => x.SquadId == mutation.SquadId && x.AssociateId == mutation.AssociateId, 
                cancellationToken: cancellationToken).ConfigureAwait(false);

        if (entity == null)
            return;
        
        adapter(entity);
    }

    public override async Task TrashOneAsync(SquadAssociateId id, CancellationToken cancellationToken = default)
    {
        var entity = await DbContext.SquadAssociates
            .SingleOrDefaultAsync(x => x.SquadId == id.SquadId && x.AssociateId == id.AssociateId,
                cancellationToken: cancellationToken).ConfigureAwait(false);
        
        if (entity != null)
            DbContext.SquadAssociates.Remove(entity);
    }
}