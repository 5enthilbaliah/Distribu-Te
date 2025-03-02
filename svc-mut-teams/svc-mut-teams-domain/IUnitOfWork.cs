namespace DistribuTe.Mutators.Teams.Domain;

public interface IUnitOfWork
{
    Task SaveChangesAsync(string mutator = "Anonymous", CancellationToken cancellationToken = default);
}