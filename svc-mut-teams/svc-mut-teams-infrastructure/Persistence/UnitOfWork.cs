namespace DistribuTe.Mutators.Teams.Infrastructure.Persistence;

using Domain;
using Microsoft.EntityFrameworkCore;

public class UnitOfWork(TeamDatabaseContext context) : IUnitOfWork
{
    private readonly TeamDatabaseContext _context = context ?? throw new ArgumentNullException(nameof(context));

    public async Task SaveChangesAsync(string mutator = "Anonymous", CancellationToken cancellationToken = default)
    {
        var entries = _context.ChangeTracker.Entries()
            .Where(e => e.Entity is IAuditableEntity)
            .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

        foreach (var entityEntry in entries)
        {
            // If the entity state is Added let's set
            // the CreatedAt and CreatedBy properties
            if (entityEntry.State == EntityState.Added)
            {
                ((IAuditableEntity)entityEntry.Entity).CreatedOn = DateTime.UtcNow;
                ((IAuditableEntity)entityEntry.Entity).CreatedBy = mutator;
            }
            else
            {
                // If the state is Modified then we don't want
                // to modify the CreatedAt and CreatedBy properties
                // so we set their state as IsModified to false
                _context.Entry((IAuditableEntity)entityEntry.Entity).Property(p => p.CreatedOn).IsModified = false;
                _context.Entry((IAuditableEntity)entityEntry.Entity).Property(p => p.CreatedBy).IsModified = false;

                // In any case we always want to set the properties
                // ModifiedAt and ModifiedBy
                ((IAuditableEntity)entityEntry.Entity).ModifiedOn = DateTime.UtcNow;
                ((IAuditableEntity)entityEntry.Entity).ModifiedBy = mutator;
            }

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}