namespace DistribuTe.Mutators.Teams.Application.Associates.Validations;

using Domain.Entities;
using Domain.Errors;
using ErrorOr;
using Framework.AppEssentials;
using MediatR;

public class TrashAssociateCommandValidationBehavior(IEntityReader<Associate, AssociateId> reader,
    IExistingEntityMarker<Associate, AssociateId> entityMarker) : 
    IPipelineBehavior<TrashAssociateCommand, ErrorOr<bool>>
{
    private readonly IEntityReader<Associate, AssociateId> _reader = 
        reader ?? throw new ArgumentNullException(nameof(reader));
    private readonly IExistingEntityMarker<Associate, AssociateId> _entityMarker = 
        entityMarker ?? throw new ArgumentNullException(nameof(entityMarker));
    
    public async Task<ErrorOr<bool>> Handle(TrashAssociateCommand request, 
        RequestHandlerDelegate<ErrorOr<bool>> next, CancellationToken cancellationToken)
    {
        var associateId = new AssociateId(request.Id);
        var existing = await _reader.PickAsync(associateId, cancellationToken);
        
        if (existing is null)
            return Errors.Associates.NotFound;
        
        _entityMarker.Id = associateId;
        _entityMarker.Entity = existing;
        
        return await next();
    }
}