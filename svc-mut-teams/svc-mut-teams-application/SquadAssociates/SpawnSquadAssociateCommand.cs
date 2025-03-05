namespace DistribuTe.Mutators.Teams.Application.SquadAssociates;

using DataContracts;
using MediatR;
using Shared;

public class SpawnSquadAssociateCommand : IRequest<SquadAssociateResponse>, IUserTrackable
{
    public SquadAssociateRequest SquadAssociate { get; set; } = null!;
    public string? User { get; set; }
}