﻿namespace DistribuTe.Mutators.Teams.Application.Associates;

using DataContracts;
using ErrorOr;
using MediatR;
using Shared;

public class CommitAssociateCommand : IRequest<ErrorOr<AssociateResponse>>, IUserTrackable
{
    public int Id { get; set; }
    public AssociateRequest Associate { get; set; } = null!;
    public string? User { get; set; }
}