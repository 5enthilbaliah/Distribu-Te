﻿namespace DistribuTe.Mutators.Teams.Infrastructure.Persistence;

using System.Linq.Expressions;
using Application;
using Domain;
using Microsoft.EntityFrameworkCore;

internal abstract class TeamsMutator<TEntity, TId>(TeamDatabaseContext context) : ITeamsMutator<TEntity, TId>
    where TEntity : class, IEntity<TId>
    where TId : class
{
    protected readonly TeamDatabaseContext DbContext = context ?? throw new ArgumentNullException(nameof(context));
        
    public void SpawnOne(TEntity entity)
    {
        DbContext.Set<TEntity>().Add(entity);
    }

    public void CommitOne(TEntity entity)
    {
        DbContext.Set<TEntity>().Update(entity);
    }

    public void TrashOne(TEntity entity)
    {
        DbContext.Set<TEntity>().Remove(entity);
    }
}

internal abstract class TeamsReader<TEntity, TId>(TeamDatabaseContext context) : ITeamsReader<TEntity, TId>
    where TEntity : class, IEntity<TId>
    where TId : class
{
    protected readonly TeamDatabaseContext DbContext = context ?? throw new ArgumentNullException(nameof(context));

    public virtual async Task<TEntity?> PickAsync(TId id, CancellationToken cancellationToken = default)
    {
        return await DbContext.Set<TEntity>()
            .SingleOrDefaultAsync(x => x.Id == id, cancellationToken: cancellationToken);
    }

    public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await DbContext.Set<TEntity>().AnyAsync(predicate, cancellationToken);
    }
}