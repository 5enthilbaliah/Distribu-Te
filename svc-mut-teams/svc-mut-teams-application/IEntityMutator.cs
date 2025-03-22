namespace DistribuTe.Mutators.Teams.Application;

using Framework.DomainEssentials;

public interface IEntityMutator<TEntity, TId>
    where TEntity : class, IEntity<TId>
    where TId : class
{
    void SpawnOne(TEntity entity);
    void CommitOne(TEntity entity);
    void TrashOne(TEntity entity);
}