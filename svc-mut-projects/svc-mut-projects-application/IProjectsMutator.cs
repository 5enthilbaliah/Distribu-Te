namespace DistribuTe.Mutators.Projects.Application;

using Framework.DomainEssentials;

public interface IProjectsMutator<TEntity, TId>
    where TEntity : class, IEntity<TId>
    where TId : class
{
    void SpawnOne(TEntity entity);
    void CommitOne(TEntity entity);
    void TrashOne(TEntity entity);
}