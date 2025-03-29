namespace DistribuTe.Aggregates.Projects.Infrastructure.Persistence;

using System.Linq.Expressions;
using Framework.AppEssentials;
using Framework.DomainEssentials;
using Microsoft.EntityFrameworkCore;

public class AggregateRepository<TEntity, TId>(ProjectSchemaDatabaseContext context) : 
    IAggregateReader<TEntity, TId>
    where TEntity : class, IEntity<TId>
    where TId : class
{
    protected readonly ProjectSchemaDatabaseContext DbContext = context ?? throw new ArgumentNullException(nameof(context));
    
    public virtual async Task<TEntity?> PickAsync(TId id, Func<IQueryable<TEntity>, IQueryable<TEntity>>? expander = null, 
        CancellationToken cancellationToken = default)
    {
        var queryable = DbContext.Set<TEntity>().AsQueryable();
        if (expander != null)
            queryable = expander(queryable);
        
        return await queryable.AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id == id, cancellationToken: cancellationToken);
    }

    public async Task<IList<TEntity>> YieldAsync(Expression<Func<TEntity, bool>>? filter, int? skip, int? take,
        Func<IQueryable<TEntity>, IQueryable<TEntity>>? expander = null, 
        Func<IQueryable<TEntity>, IQueryable<TEntity>>? sorter = null, 
        CancellationToken cancellationToken = default)
    {
        var queryable = DbContext.Set<TEntity>().AsQueryable();
        if (expander != null)
            queryable = expander(queryable);
        
        queryable = queryable.AsNoTracking();
        
        if (filter != null)
            queryable = queryable.Where(filter);
        else
            take = 500;
        
        if (sorter != null)
            queryable = sorter(queryable);
        
        if (skip.HasValue)
            queryable = queryable.Skip(skip.Value);
        
        if (take.HasValue)
            queryable = queryable.Take(take.Value);
        
        return await queryable.ToListAsync(cancellationToken);
    }

    public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken = default)
    {
        return await DbContext.Set<TEntity>().AnyAsync(filter, cancellationToken);
    }

    public async Task<long> CountAsync(Expression<Func<TEntity, bool>>? filter, CancellationToken cancellationToken = default)
    {
        var queryable = DbContext.Set<TEntity>().AsNoTracking();
        if (filter != null)
            queryable = queryable.Where(filter);
        
        return await queryable.LongCountAsync(cancellationToken: cancellationToken)
            .ConfigureAwait(false);
    }
}