namespace DistribuTe.Mutators.Teams.Application.Associates.Validations;

using Domain.Entities;
using Domain.Errors;
using ErrorOr;
using MediatR;

public class TrashAssociateCommandValidationBehavior(ITeamsReader<Associate, AssociateId> reader) : 
    IPipelineBehavior<TrashAssociateCommand, ErrorOr<bool>>
{
    private readonly ITeamsReader<Associate, AssociateId> _reader = 
        reader ?? throw new ArgumentNullException(nameof(reader));
    
    public async Task<ErrorOr<bool>> Handle(TrashAssociateCommand request, 
        RequestHandlerDelegate<ErrorOr<bool>> next, CancellationToken cancellationToken)
    {
        var associateId = new AssociateId(request.Id);
        var existing = await _reader.PickAsync(associateId, cancellationToken);
        
        if (existing is null)
            return Errors.Associates.NotFound;
        
        request.ToDelete = existing;
        
        return await next();
    }
}