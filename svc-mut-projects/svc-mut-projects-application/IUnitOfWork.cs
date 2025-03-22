namespace DistribuTe.Mutators.Projects.Application;

public interface IUnitOfWork
{
    Task SaveChangesAsync(string mutator = "Anonymous", CancellationToken cancellationToken = default);
}