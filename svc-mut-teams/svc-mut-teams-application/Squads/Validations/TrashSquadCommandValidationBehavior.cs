namespace DistribuTe.Mutators.Teams.Application.Squads.Validations;

using Domain.Entities;
using Domain.Errors;
using ErrorOr;
using MediatR;
using Shared;

public class TrashSquadCommandValidationBehavior(ITeamsReader<Squad, SquadId> reader,
    IExistingEntityMarker<Squad, SquadId> entityMarker) : 
    IPipelineBehavior<TrashSquadCommand, ErrorOr<bool>>
{
    private readonly ITeamsReader<Squad, SquadId> _reader = 
        reader ?? throw new ArgumentNullException(nameof(reader));
    private readonly IExistingEntityMarker<Squad, SquadId> _entityMarker = 
        entityMarker ?? throw new ArgumentNullException(nameof(entityMarker));
    
    public async Task<ErrorOr<bool>> Handle(TrashSquadCommand request, 
        RequestHandlerDelegate<ErrorOr<bool>> next, CancellationToken cancellationToken)
    {
        var squadId = new SquadId(request.Id);
        var existing = await _reader.PickAsync(squadId, cancellationToken);
        
        if (existing is null)
            return Errors.Squads.NotFound;
        
        _entityMarker.Id = squadId;
        _entityMarker.Entity = existing;
        
        return await next();
    }
}