namespace DistribuTe.Aggregates.Teams.Application;

using System.Linq.Expressions;
using Domain;

public interface ITeamsReader<TEntity, TId> 
    where TEntity : class, IEntity<TId>
    where TId : class
{
    Task<TEntity?> PickAsync(TId id, CancellationToken cancellationToken = default);
    Task<IList<TEntity>> YieldAsync(Expression<Func<TEntity, bool>>? filter = null, int skip = 0, int take = 500, 
        CancellationToken cancellationToken = default);
    Task<bool> AnyAsync(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken = default);
    Task<long> CountAsync(Expression<Func<TEntity, bool>>? filter, CancellationToken cancellationToken = default);
}