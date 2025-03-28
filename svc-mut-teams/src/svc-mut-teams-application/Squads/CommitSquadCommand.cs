﻿namespace DistribuTe.Mutators.Teams.Application.Squads;

using DataContracts;
using ErrorOr;
using Framework.AppEssentials;
using MediatR;

public class CommitSquadCommand : IRequest<ErrorOr<SquadResponse>>, IUserTrackable
{
    public int Id { get; set; }
    public SquadRequest Squad { get; set; } = null!;
    public string? User { get; set; }
}