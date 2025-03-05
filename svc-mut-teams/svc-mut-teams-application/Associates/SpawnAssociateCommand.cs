namespace DistribuTe.Mutators.Teams.Application.Associates;

using DataContracts;
using MediatR;
using Shared;

public class SpawnAssociateCommand : IRequest<AssociateResponse>, IUserTrackable
{
    public AssociateRequest Associate { get; set; } = null!;
    public string? User { get; set; }
}