namespace DistribuTe.Framework.AppEssentials;

using System.Linq.Expressions;
using DomainEssentials;

public interface IEntityReader<TEntity, TId> 
    where TEntity : class, IEntity<TId>
    where TId : class
{
    Task<TEntity?> PickAsync(TId id, CancellationToken cancellationToken = default);
    Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
}