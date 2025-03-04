namespace DistribuTe.Mutators.Teams.Application;

public interface IUnitOfWork
{
    Task SaveChangesAsync(string mutator = "Anonymous", CancellationToken cancellationToken = default);
}