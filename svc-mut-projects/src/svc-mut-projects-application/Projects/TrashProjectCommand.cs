namespace DistribuTe.Mutators.Projects.Application.Projects;

using ErrorOr;
using MediatR;
using Shared;

public class TrashProjectCommand : IRequest<ErrorOr<bool>>, IUserTrackable
{
    public int Id { get; set; }
    public string? User { get; set; }
}