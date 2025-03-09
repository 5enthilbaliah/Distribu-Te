namespace DistribuTe.Mutators.Teams.Application.SquadAssociates.Validations;

using DataContracts;
using Domain.Entities;
using Domain.Errors;
using ErrorOr;
using FluentValidation;
using MediatR;
using Shared;

public class SpawnSquadAssociateCommandValidationBehavior(ITeamsReader<SquadAssociate, SquadAssociateId> reader,
    IValidator<SquadAssociateRequest> validator) : DistribuTeRequestValidationBehavior<SquadAssociateRequest>(validator),
    IPipelineBehavior<SpawnSquadAssociateCommand, ErrorOr<SquadAssociateResponse>>
{
    private readonly ITeamsReader<SquadAssociate, SquadAssociateId> _reader = 
        reader ?? throw new ArgumentNullException(nameof(reader));
    
    public async Task<ErrorOr<SquadAssociateResponse>> Handle(SpawnSquadAssociateCommand request, 
        RequestHandlerDelegate<ErrorOr<SquadAssociateResponse>> next, CancellationToken cancellationToken)
    {
        var (success, errors) = Validate(request.SquadAssociate);
        if (!success)
            return errors;
        var squadId = new SquadId(request.SquadAssociate.Squad_Id);
        var associateId = new AssociateId(request.SquadAssociate.Associate_Id);
        var emailFound = await _reader.AnyAsync(a => a.SquadId == squadId
                                                     && a.AssociateId == associateId && !a.EndedOn.HasValue, cancellationToken);

        if (emailFound)
            return Errors.SquadAssociates.DuplicateAllocation;
        
        var response = await next();
        return response;
    }
}