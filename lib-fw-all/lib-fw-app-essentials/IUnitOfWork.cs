namespace DistribuTe.Framework.AppEssentials;

public interface IUnitOfWork
{
    Task SaveChangesAsync(string mutator = "Anonymous", CancellationToken cancellationToken = default);
}