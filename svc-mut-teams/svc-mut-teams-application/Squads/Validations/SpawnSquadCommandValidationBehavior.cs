namespace DistribuTe.Mutators.Teams.Application.Squads.Validations;

using DataContracts;
using Domain.Entities;
using Domain.Errors;
using ErrorOr;
using FluentValidation;
using Framework.AppEssentials.Validations;
using MediatR;

public class SpawnSquadCommandValidationBehavior(IEntityReader<Squad, SquadId> reader,
    IValidator<SquadRequest> validator) : DistribuTeRequestValidationBehavior<SquadRequest>(validator),
    IPipelineBehavior<SpawnSquadCommand, ErrorOr<SquadResponse>>
{
    private readonly IEntityReader<Squad, SquadId> _reader = 
        reader ?? throw new ArgumentNullException(nameof(reader));
    
    public async Task<ErrorOr<SquadResponse>> Handle(SpawnSquadCommand request, 
        RequestHandlerDelegate<ErrorOr<SquadResponse>> next, CancellationToken cancellationToken)
    {
        var (success, errors) = Validate(request.Squad);
        if (!success)
            return errors;
        
        var codeFound = await _reader.AnyAsync(a => a.Code == request.Squad.Code,
            cancellationToken);
        var nameFound = await _reader.AnyAsync(a => a.Name == request.Squad.Name,
            cancellationToken);

        if (codeFound)
            return Errors.Squads.DuplicateCode;
        if (nameFound)
            return Errors.Squads.DuplicateName;
        
        var response = await next();
        return response;
    }
}