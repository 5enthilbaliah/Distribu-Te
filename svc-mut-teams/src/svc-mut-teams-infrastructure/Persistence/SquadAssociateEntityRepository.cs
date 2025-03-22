namespace DistribuTe.Mutators.Teams.Infrastructure.Persistence;

using System.Diagnostics.CodeAnalysis;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

internal sealed class SquadAssociateEntityRepository(TeamSchemaDatabaseContext context)
    : EntityRepository<SquadAssociate, SquadAssociateId>(context)
{
    [ExcludeFromCodeCoverage]
    public override async Task<SquadAssociate?> PickAsync(SquadAssociateId id,
        CancellationToken cancellationToken = default)
    {
        return await DbContext.SquadAssociates
            .SingleOrDefaultAsync(x => x.SquadId == id.SquadId && x.AssociateId == id.AssociateId, 
                cancellationToken: cancellationToken);
    }
}