namespace DistribuTe.Mutators.Teams.Application;

using Domain;

public abstract class BaseTrashCommand<TEntity, TId>
    where TEntity : class, IEntity<TId>
    where TId : class
{
    internal TEntity? ToDelete { get; set; }
}