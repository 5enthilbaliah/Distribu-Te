﻿namespace DistribuTe.Mutators.Teams.Application.SquadAssociates;

using MediatR;
using Models;
using Shared;

public class CommitSquadAssociateCommand : IRequest<SquadAssociateVm>, IUserTrackable
{
    public int SquadId { get; set; }
    public int AssociateId { get; set; }
    public SquadAssociateRm SquadAssociate { get; set; } = null!;
    public string? User { get; set; }
}