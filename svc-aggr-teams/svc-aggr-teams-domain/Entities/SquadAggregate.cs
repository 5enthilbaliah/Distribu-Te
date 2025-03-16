namespace DistribuTe.Aggregates.Teams.Domain.Entities;

using DistribuTe.Domain.TeamEntities;
using Framework.DomainEssentials;

public record SquadId(int Value);

public class SquadAggregate : BaseSquad, IEntity<SquadId>
{
    public SquadId Id { get; set; }  = null!;
    public virtual IList<SquadAssociateAggregate> SquadAssociates { get; set; }
}