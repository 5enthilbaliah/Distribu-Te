namespace DistribuTe.Mutators.Teams.Application.SquadAssociates.Validations;

using Domain.Entities;
using Domain.Errors;
using ErrorOr;
using MediatR;
using Shared;

public class TrashSquadAssociateCommandValidationBehavior(ITeamsReader<SquadAssociate, SquadAssociateId> reader,
    IExistingEntityMarker<SquadAssociate, SquadAssociateId> entityMarker) : 
    IPipelineBehavior<TrashSquadAssociateCommand, ErrorOr<bool>>
{
    private readonly ITeamsReader<SquadAssociate, SquadAssociateId> _reader = 
        reader ?? throw new ArgumentNullException(nameof(reader));
    private readonly IExistingEntityMarker<SquadAssociate, SquadAssociateId> _entityMarker = 
        entityMarker ?? throw new ArgumentNullException(nameof(entityMarker));
    
    public async Task<ErrorOr<bool>> Handle(TrashSquadAssociateCommand request, 
        RequestHandlerDelegate<ErrorOr<bool>> next, CancellationToken cancellationToken)
    {
        var squadId = new SquadId(request.SquadId);
        var associateId = new AssociateId(request.AssociateId);
        var squadAssociateId = new SquadAssociateId(squadId, associateId);
        var existing = await _reader.PickAsync(squadAssociateId, cancellationToken);
        
        if (existing is null)
            return Errors.SquadAssociates.NotFound;
        
        _entityMarker.Id = squadAssociateId;
        _entityMarker.Entity = existing;
        
        return await next();
    }
}