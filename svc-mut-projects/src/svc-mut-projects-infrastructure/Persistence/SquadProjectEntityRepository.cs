namespace DistribuTe.Mutators.Projects.Infrastructure.Persistence;

using System.Diagnostics.CodeAnalysis;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

internal sealed class SquadProjectEntityRepository(ProjectSchemaDatabaseContext context)
    : EntityRepository<SquadProject, SquadProjectId>(context)
{
    [ExcludeFromCodeCoverage]
    public override async Task<SquadProject?> PickAsync(SquadProjectId id,
        CancellationToken cancellationToken = default)
    {
        return await DbContext.SquadProjects
            .SingleOrDefaultAsync(x => x.SquadId == id.SquadId && x.ProjectId == id.ProjectId, 
                cancellationToken: cancellationToken);
    }
}