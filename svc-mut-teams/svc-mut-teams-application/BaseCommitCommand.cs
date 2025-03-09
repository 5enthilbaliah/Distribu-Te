namespace DistribuTe.Mutators.Teams.Application;

using Domain;

public abstract class BaseCommitCommand<TEntity, TId>
    where TEntity : class, IEntity<TId>
    where TId : class
{
    internal TEntity? ToMutate { get; set; }
}