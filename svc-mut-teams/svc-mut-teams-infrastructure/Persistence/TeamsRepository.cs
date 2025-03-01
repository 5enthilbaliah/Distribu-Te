namespace DistribuTe.Mutators.Teams.Infrastructure.Persistence;

using Domain;
using Domain.Entities;

internal abstract class TeamsRepository<TEntity, TId>(TeamDatabaseContext context) : ITeamsRepository<TEntity, TId>
    where TEntity : class, IEntity<TId>
    where TId : struct, IEquatable<TId>
{
    protected readonly TeamDatabaseContext DbContext = context ?? throw new ArgumentNullException(nameof(context));
        
    public void SpawnOne(TEntity entity)
    {
        DbContext.Set<TEntity>().Add(entity);
    }

    public void CommitOne(TId id, TEntity entity)
    {
        if (id.Equals(entity.Id))
            DbContext.Set<TEntity>().Update(entity);
    }

    public void TrashOne(TId id)
    {
        var entity = DbContext.Set<TEntity>().FirstOrDefault(x => x.Id.Equals(id));
        
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