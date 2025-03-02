namespace DistribuTe.Mutators.Teams.Infrastructure.Persistence;

using Domain;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

internal abstract class TeamsRepository<TEntity, TId>(TeamDatabaseContext context) : ITeamsRepository<TEntity, TId>
    where TEntity : class, IEntity<TId>
    where TId : class
{
    protected readonly TeamDatabaseContext DbContext = context ?? throw new ArgumentNullException(nameof(context));
        
    public void SpawnOne(TEntity entity)
    {
        DbContext.Set<TEntity>().Add(entity);
    }

    public virtual async Task CommitOneAsync(TEntity mutation, Func<TEntity, TEntity> adapter,
        CancellationToken cancellationToken = default)
    {
        var entity = await DbContext.Set<TEntity>()
            .SingleOrDefaultAsync(x => x.Id == mutation.Id, cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        if (entity == null)
            return;
        
        adapter(entity);
    }

    public virtual async Task TrashOneAsync(TId id, CancellationToken cancellationToken = default)
    {
        var entity = await DbContext.Set<TEntity>().SingleOrDefaultAsync(x => x.Id == id, 
            cancellationToken);
        
        if (entity != null)
            DbContext.Set<TEntity>().Remove(entity);
    }
}

internal sealed class AssociateTeamsRepository(TeamDatabaseContext context) 
    : TeamsRepository<Associate, AssociateId>(context)
{ }

internal sealed class SquadTeamsRepository(TeamDatabaseContext context) 
    : TeamsRepository<Squad, SquadId>(context)
{ }

internal sealed class SquadAssociateTeamsRepository(TeamDatabaseContext context) 
    : TeamsRepository<SquadAssociate, SquadAssociateId>(context)
{ }