﻿namespace DistribuTe.Mutators.Teams.Infrastructure.Persistence;

using System.Linq.Expressions;
using Framework.AppEssentials;
using Framework.DomainEssentials;
using Microsoft.EntityFrameworkCore;

internal abstract class EntityRepository<TEntity, TId>(TeamSchemaDatabaseContext context) : 
    IEntityMutator<TEntity, TId>, IEntityReader<TEntity, TId>
    where TEntity : class, IEntity<TId>
    where TId : class
{
    protected readonly TeamSchemaDatabaseContext DbContext = context ?? throw new ArgumentNullException(nameof(context));
        
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