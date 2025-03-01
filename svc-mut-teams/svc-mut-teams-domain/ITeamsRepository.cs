namespace DistribuTe.Mutators.Teams.Domain;

public interface ITeamsRepository<TEntity, TId>
    where TEntity : class, IEntity<TId>
    where TId : struct, IEquatable<TId>
{
    void SpawnOne(TEntity entity);
    void CommitOne(TId id, TEntity entity);
    void TrashOne(TId id);
}