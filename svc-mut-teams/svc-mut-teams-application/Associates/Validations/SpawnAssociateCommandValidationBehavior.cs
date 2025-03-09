namespace DistribuTe.Mutators.Teams.Application.Associates.Validations;

using DataContracts;
using Domain.Entities;
using Domain.Errors;
using ErrorOr;
using FluentValidation;
using MediatR;

public class SpawnAssociateCommandValidationBehavior(ITeamsReader<Associate, AssociateId> reader,
    IValidator<AssociateRequest> validator) : AssociateRequestValidationBehavior(validator),
    IPipelineBehavior<SpawnAssociateCommand, ErrorOr<AssociateResponse>>
{
    private readonly ITeamsReader<Associate, AssociateId> _reader = 
        reader ?? throw new ArgumentNullException(nameof(reader));
    
    public async Task<ErrorOr<AssociateResponse>> Handle(SpawnAssociateCommand request, 
        RequestHandlerDelegate<ErrorOr<AssociateResponse>> next, CancellationToken cancellationToken)
    {
        var (success, errors) = Validate(request.Associate);
        if (!success)
            return errors;
        
        var emailFound = await _reader.AnyAsync(a => a.EmailId == request.Associate.Email_Id,
            cancellationToken);

        if (emailFound)
            return Errors.Associates.DuplicateEmail;
        
        var response = await next();
        return response;
    }
}