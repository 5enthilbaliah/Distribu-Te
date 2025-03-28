﻿namespace DistribuTe.Mutators.Teams.Domain.Entities;

using DistribuTe.Domain.TeamEntities;
using Framework.DomainEssentials;

public record SquadId(int Value);

public class Squad : BaseSquad, IEntity<SquadId>, IAuditableEntity
{
    public SquadId Id { get; set; }  = null!;
}