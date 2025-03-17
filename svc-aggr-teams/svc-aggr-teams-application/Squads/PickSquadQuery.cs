namespace DistribuTe.Aggregates.Teams.Application.Squads;

using DataContracts;
using ErrorOr;
using Framework.AppEssentials.Implementations;
using MediatR;

public class PickSquadQuery : IRequest<ErrorOr<SquadModel>>
{
    public int Id { get; set; }
    public WhereClauseFacade WhereClauseFacade { get; init; } = null!;
}