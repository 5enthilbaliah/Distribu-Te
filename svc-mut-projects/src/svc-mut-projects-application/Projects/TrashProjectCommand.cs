namespace DistribuTe.Mutators.Projects.Application.Projects;

using ErrorOr;
using Framework.AppEssentials;
using MediatR;

public class TrashProjectCommand : IRequest<ErrorOr<bool>>, IUserTrackable
{
    public int Id { get; set; }
    public string? User { get; set; }
}