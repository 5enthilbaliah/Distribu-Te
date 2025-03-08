namespace DistribuTe.Mutators.Teams.Infrastructure.Persistence;

using Domain.Entities;
using Microsoft.EntityFrameworkCore;

internal sealed class SquadAssociateTeamsMutator(TeamDatabaseContext context)
    : TeamsMutator<SquadAssociate, SquadAssociateId>(context)
{ }

internal sealed class SquadAssociateTeamsReader(TeamDatabaseContext context)
    : TeamsReader<SquadAssociate, SquadAssociateId>(context)
{
    public override async Task<SquadAssociate?> PickAsync(SquadAssociateId id,
        CancellationToken cancellationToken = default)
    {
        return await DbContext.SquadAssociates
            .SingleOrDefaultAsync(x => x.SquadId == id.SquadId && x.AssociateId == id.AssociateId,
                cancellationToken: cancellationToken);
    }
}