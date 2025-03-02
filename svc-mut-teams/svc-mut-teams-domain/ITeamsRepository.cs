namespace DistribuTe.Mutators.Teams.Domain;

public interface ITeamsRepository<TEntity, TId>
    where TEntity : class, IEntity<TId>
    where TId : class
{
    void SpawnOne(TEntity entity);
    Task CommitOneAsync(TEntity mutation, Func<TEntity, TEntity> adapter,
        CancellationToken cancellationToken = default);
    Task TrashOneAsync(TId id, CancellationToken cancellationToken = default);
}