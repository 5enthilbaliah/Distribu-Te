namespace DistribuTe.Mutators.Teams.Infrastructure.Persistence;

using Domain.Entities;
using Microsoft.EntityFrameworkCore;

internal sealed class SquadAssociateTeamsRepository(TeamDatabaseContext context)
    : TeamsRepository<SquadAssociate, SquadAssociateId>(context)
{
    public override async Task<SquadAssociate?> PickAsync(SquadAssociateId id,
        CancellationToken cancellationToken = default)
    {
        return await DbContext.SquadAssociates
            .SingleOrDefaultAsync(x => x.SquadId == id.SquadId && x.AssociateId == id.AssociateId
                                                               && !x.EndedOn.HasValue, cancellationToken: cancellationToken);
    }
}