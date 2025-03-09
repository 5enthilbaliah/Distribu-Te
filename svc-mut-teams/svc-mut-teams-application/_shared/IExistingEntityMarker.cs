// ReSharper disable once CheckNamespace
namespace DistribuTe.Mutators.Teams.Application.Shared;

using Domain;

public interface IExistingEntityMarker<TEntity, TId>
    where TEntity : class, IEntity<TId>
    where TId : class
{
    TId? Id { get; set; }
    TEntity? Entity { get; set; }
}