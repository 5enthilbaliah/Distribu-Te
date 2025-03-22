namespace DistribuTe.Framework.AppEssentials;

using System.Linq.Expressions;
using DomainEssentials;

public interface IEntityMutator<TEntity, TId>
    where TEntity : class, IEntity<TId>
    where TId : class
{
    void SpawnOne(TEntity entity);
    void CommitOne(TEntity entity);
    void TrashOne(TEntity entity);
}

public interface IEntityReader<TEntity, TId> 
    where TEntity : class, IEntity<TId>
    where TId : class
{
    Task<TEntity?> PickAsync(TId id, CancellationToken cancellationToken = default);
    Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
}

public interface IUnitOfWork
{
    Task SaveChangesAsync(string mutator = "Anonymous", CancellationToken cancellationToken = default);
}