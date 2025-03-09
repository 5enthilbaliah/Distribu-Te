namespace DistribuTe.Mutators.Teams.Application;

using Domain;

public interface ITeamsMutator<TEntity, TId>
    where TEntity : class, IEntity<TId>
    where TId : class
{
    void SpawnOne(TEntity entity);
    void CommitOne(TEntity entity);
    void TrashOne(TEntity entity);
}