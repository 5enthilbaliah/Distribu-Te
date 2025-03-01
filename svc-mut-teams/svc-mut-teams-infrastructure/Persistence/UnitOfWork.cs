namespace DistribuTe.Mutators.Teams.Infrastructure.Persistence;

using Domain;

public class UnitOfWork(TeamDatabaseContext context) : IUnitOfWork
{
    private readonly TeamDatabaseContext _context = context ?? throw new ArgumentNullException(nameof(context));
    
    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}