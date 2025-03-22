namespace DistribuTe.Mutators.Projects.Infrastructure.Persistence;

using System.Diagnostics.CodeAnalysis;
using Framework.AppEssentials;
using Framework.DomainEssentials;

[ExcludeFromCodeCoverage]
public class ExistingEntityMarker<TEntity, TId> : IExistingEntityMarker<TEntity, TId>
    where TEntity : class, IEntity<TId>
    where TId : class
{
    public TId? Id { get; set; }
    public TEntity? Entity { get; set; }
}