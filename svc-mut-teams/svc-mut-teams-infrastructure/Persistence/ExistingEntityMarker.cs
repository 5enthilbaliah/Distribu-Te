namespace DistribuTe.Mutators.Teams.Infrastructure.Persistence;

using Application.Shared;
using Domain;

public class ExistingEntityMarker<TEntity, TId> : IExistingEntityMarker<TEntity, TId>
    where TEntity : class, IEntity<TId>
    where TId : class
{
    public TId? Id { get; set; }
    public TEntity? Entity { get; set; }
}