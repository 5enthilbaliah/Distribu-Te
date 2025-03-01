namespace DistribuTe.Mutators.Teams.Domain.Entities;

using DistribuTe.Domain.AppEntities;

public readonly record struct SquadId(int Value);

public class Squad : BaseSquad, IEntity<SquadId>
{
    public SquadId Id { get; set; }
}