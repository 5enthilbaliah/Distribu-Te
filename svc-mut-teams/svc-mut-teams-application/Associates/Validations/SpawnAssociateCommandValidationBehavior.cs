namespace DistribuTe.Mutators.Teams.Application.Associates.Validations;

using DataContracts;
using Domain.Entities;
using Domain.Errors;
using ErrorOr;
using FluentValidation;
using Framework.AppEssentials.Validations;
using MediatR;
using Shared;

public class SpawnAssociateCommandValidationBehavior(IEntityReader<Associate, AssociateId> reader,
    IValidator<AssociateRequest> validator) : DistribuTeRequestValidationBehavior<AssociateRequest>(validator),
    IPipelineBehavior<SpawnAssociateCommand, ErrorOr<AssociateResponse>>
{
    private readonly IEntityReader<Associate, AssociateId> _reader = 
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