namespace DistribuTe.Framework.AppEssentials;

using DomainEssentials;

public interface IExistingEntityMarker<TEntity, TId>
    where TEntity : class, IEntity<TId>
    where TId : class
{
    TId? Id { get; set; }
    TEntity? Entity { get; set; }
}