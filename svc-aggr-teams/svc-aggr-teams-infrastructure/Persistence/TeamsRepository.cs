namespace DistribuTe.Aggregates.Teams.Infrastructure.Persistence;

using System.Linq.Expressions;
using Application;
using Framework.DomainEssentials;
using Microsoft.EntityFrameworkCore;

public class TeamsRepository<TEntity, TId>(TeamDatabaseContext context) : 
    ITeamsReader<TEntity, TId>
    where TEntity : class, IEntity<TId>
    where TId : class
{
    protected readonly TeamDatabaseContext DbContext = context ?? throw new ArgumentNullException(nameof(context));
    
    public virtual async Task<TEntity?> PickAsync(TId id, CancellationToken cancellationToken = default)
    {
        return await DbContext.Set<TEntity>()
            .SingleOrDefaultAsync(x => x.Id == id, cancellationToken: cancellationToken);
    }

    public async Task<IList<TEntity>> YieldAsync(Expression<Func<TEntity, bool>>? filter = null, int skip = 0, int take = 500,
        CancellationToken cancellationToken = default)
    {
        var queryable = DbContext.Set<TEntity>().AsQueryable();
        if (filter != null)
            queryable = queryable.Where(filter);
        queryable = queryable.Skip(skip);
        queryable = queryable.Take(take);
        
        return await queryable.ToListAsync(cancellationToken);
    }

    public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken = default)
    {
        return await DbContext.Set<TEntity>().AnyAsync(filter, cancellationToken);
    }

    public async Task<long> CountAsync(Expression<Func<TEntity, bool>>? filter, CancellationToken cancellationToken = default)
    {
        var queryable = DbContext.Set<TEntity>().AsQueryable();
        if (filter != null)
            queryable = queryable.Where(filter);
        
        return await queryable.LongCountAsync(cancellationToken: cancellationToken)
            .ConfigureAwait(false);
    }
}