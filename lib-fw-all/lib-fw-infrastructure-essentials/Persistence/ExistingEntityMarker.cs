namespace DistribuTe.Framework.InfrastructureEssentials.Persistence;

using System.Diagnostics.CodeAnalysis;
using AppEssentials;
using DomainEssentials;

[ExcludeFromCodeCoverage]
public class ExistingEntityMarker<TEntity, TId> : IExistingEntityMarker<TEntity, TId>
    where TEntity : class, IEntity<TId>
    where TId : class
{
    public TId? Id { get; set; }
    public TEntity? Entity { get; set; }
}