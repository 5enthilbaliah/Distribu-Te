namespace DistribuTe.Framework.AppEssentials;

using DomainEssentials;

public interface IEntityMutator<TEntity, TId>
    where TEntity : class, IEntity<TId>
    where TId : class
{
    void SpawnOne(TEntity entity);
    void CommitOne(TEntity entity);
    void TrashOne(TEntity entity);
}