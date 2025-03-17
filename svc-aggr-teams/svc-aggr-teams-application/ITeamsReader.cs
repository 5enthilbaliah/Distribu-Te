﻿namespace DistribuTe.Aggregates.Teams.Application;

using System.Linq.Expressions;
using Framework.DomainEssentials;

public interface ITeamsReader<TEntity, TId> 
    where TEntity : class, IEntity<TId>
    where TId : class
{
    Task<TEntity?> PickAsync(TId id, Func<IQueryable<TEntity>, IQueryable<TEntity>>? expander = null, 
        CancellationToken cancellationToken = default);
    Task<IList<TEntity>> YieldAsync(Expression<Func<TEntity, bool>>? filter, int skip, int take, 
        Func<IQueryable<TEntity>, IQueryable<TEntity>>? expander = null, CancellationToken cancellationToken = default);
    Task<bool> AnyAsync(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken = default);
    Task<long> CountAsync(Expression<Func<TEntity, bool>>? filter, CancellationToken cancellationToken = default);
}