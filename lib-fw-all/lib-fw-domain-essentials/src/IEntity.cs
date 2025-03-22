namespace DistribuTe.Framework.DomainEssentials;

public interface IEntity<TId>
    where TId : class
{
    public TId Id { get; set; }
}