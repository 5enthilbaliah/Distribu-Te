﻿namespace DistribuTe.Mutators.Teams.Application;

using System.Linq.Expressions;
using Framework.DomainEssentials;

public interface ITeamsReader<TEntity, TId> 
    where TEntity : class, IEntity<TId>
    where TId : class
{
    Task<TEntity?> PickAsync(TId id, CancellationToken cancellationToken = default);
    Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
}