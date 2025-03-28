﻿namespace DistribuTe.Mutators.Projects.Application.Projects;

using DataContracts;
using ErrorOr;
using Framework.AppEssentials;
using MediatR;

public class CommitProjectCommand : IRequest<ErrorOr<ProjectResponse>>, IUserTrackable
{
    public int Id { get; set; }
    public ProjectRequest Project { get; set; } = null!;
    public string? User { get; set; }
}