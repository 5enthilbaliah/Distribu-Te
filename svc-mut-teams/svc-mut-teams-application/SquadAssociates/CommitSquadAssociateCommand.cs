namespace DistribuTe.Mutators.Teams.Application.SquadAssociates;

using MediatR;
using Models;
using Shared;

public class SpawnSquadAssociateCommand : IRequest<SquadAssociateVm>, IUserTrackable
{
    public SquadAssociateRm SquadAssociate { get; set; } = null!;
    public string? User { get; set; }
}