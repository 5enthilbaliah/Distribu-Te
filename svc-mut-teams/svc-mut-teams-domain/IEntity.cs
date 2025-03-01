namespace DistribuTe.Mutators.Teams.Domain;

public interface IEntity<TId>
    where TId : struct
{
    public TId Id { get; set; }
}