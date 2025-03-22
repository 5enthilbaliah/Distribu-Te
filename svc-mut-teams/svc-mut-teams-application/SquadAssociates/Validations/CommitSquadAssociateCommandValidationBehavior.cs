namespace DistribuTe.Mutators.Teams.Application.SquadAssociates.Validations;

using DataContracts;
using Domain.Entities;
using Domain.Errors;
using ErrorOr;
using FluentValidation;
using Framework.AppEssentials;
using Framework.AppEssentials.Validations;
using MediatR;

public class CommitSquadAssociateCommandValidationBehavior(ITeamsReader<SquadAssociate, SquadAssociateId> reader,
    IValidator<SquadAssociateRequest> validator, IExistingEntityMarker<SquadAssociate, SquadAssociateId> entityMarker) : 
    DistribuTeRequestValidationBehavior<SquadAssociateRequest>(validator),
    IPipelineBehavior<CommitSquadAssociateCommand, ErrorOr<SquadAssociateResponse>>
{
    private readonly ITeamsReader<SquadAssociate, SquadAssociateId> _reader =
        reader ?? throw new ArgumentNullException(nameof(reader));
    private readonly IExistingEntityMarker<SquadAssociate, SquadAssociateId> _entityMarker = 
        entityMarker ?? throw new ArgumentNullException(nameof(entityMarker));


    public async Task<ErrorOr<SquadAssociateResponse>> Handle(CommitSquadAssociateCommand request,
        RequestHandlerDelegate<ErrorOr<SquadAssociateResponse>> next, CancellationToken cancellationToken)
    {
        var (success, errors) = Validate(request.SquadAssociate);
        if (!success)
            return errors;
        
        var squadId = new SquadId(request.SquadId);
        var associateId = new AssociateId(request.AssociateId);
        var squadAssociateId = new SquadAssociateId(squadId, associateId);
        var existing = await _reader.PickAsync(squadAssociateId, cancellationToken);

        if (existing is null)
            return Errors.SquadAssociates.NotFound;

        _entityMarker.Id = squadAssociateId;
        _entityMarker.Entity = existing;

        var response = await next();
        return response;
    }
}