﻿namespace DistribuTe.Aggregates.Teams.Application.SquadAssociates;

using DataContracts;
using ErrorOr;
using Framework.AppEssentials.Linq;
using MediatR;

public class PickSquadAssociateQuery : IRequest<ErrorOr<SquadAssociateModel>>
{
    public int SquadId { get; set; }
    public int AssociateId { get; set; }
    public EntityLinqFacade EntityLinqFacade { get; init; } = null!;
}