namespace DistribuTe.Mutators.Teams.Application.SquadAssociates.Validations;

using DataContracts;
using Domain.Entities;
using Domain.Errors;
using ErrorOr;
using FluentValidation;
using MediatR;
using Shared;

public class SpawnSquadAssociateCommandValidationBehavior(ITeamsReader<SquadAssociate, SquadAssociateId> reader,
    IValidator<SquadAssociateRequest> validator, ITeamsReader<Squad, SquadId> squadReader,
    ITeamsReader<Associate, AssociateId> associateReader) : 
    DistribuTeRequestValidationBehavior<SquadAssociateRequest>(validator),
    IPipelineBehavior<SpawnSquadAssociateCommand, ErrorOr<SquadAssociateResponse>>
{
    private readonly ITeamsReader<SquadAssociate, SquadAssociateId> _reader = 
        reader ?? throw new ArgumentNullException(nameof(reader));
    private readonly ITeamsReader<Squad, SquadId> _squadReader = 
        squadReader ?? throw new ArgumentNullException(nameof(squadReader));
    private readonly ITeamsReader<Associate, AssociateId> _associateReader = 
        associateReader ?? throw new ArgumentNullException(nameof(associateReader));
    
    public async Task<ErrorOr<SquadAssociateResponse>> Handle(SpawnSquadAssociateCommand request, 
        RequestHandlerDelegate<ErrorOr<SquadAssociateResponse>> next, CancellationToken cancellationToken)
    {
        var (success, errors) = Validate(request.SquadAssociate);
        if (!success)
            return errors;
        var squadId = new SquadId(request.SquadAssociate.Squad_Id);
        var associateId = new AssociateId(request.SquadAssociate.Associate_Id);
        
        var squadFound = await _squadReader.AnyAsync(a => a.Id == squadId,
            cancellationToken);
        if (!squadFound)
            return Errors.Squads.NotFound;
        
        var associateFound = await _associateReader.AnyAsync(a => a.Id == associateId,
            cancellationToken);
        if (!associateFound)
            return Errors.Associates.NotFound;
        
        var allocationFound = await _reader.AnyAsync(a => a.SquadId == squadId
                                                     && a.AssociateId == associateId && !a.EndedOn.HasValue, cancellationToken);
        if (allocationFound)
            return Errors.SquadAssociates.DuplicateAllocation;
        
        var response = await next();
        return response;
    }
}