namespace DistribuTe.Mutators.Teams.Application.Associates.Validations;

using DataContracts;
using Domain.Entities;
using Domain.Errors;
using ErrorOr;
using FluentValidation;
using MediatR;

public class CommitAssociateCommandValidationBehavior(ITeamsReader<Associate, AssociateId> reader,
    IValidator<AssociateRequest> validator) : AssociateRequestValidationBehavior(validator),
    IPipelineBehavior<CommitAssociateCommand, ErrorOr<AssociateResponse>>
{
    private readonly ITeamsReader<Associate, AssociateId> _reader =
        reader ?? throw new ArgumentNullException(nameof(reader));


    public async Task<ErrorOr<AssociateResponse>> Handle(CommitAssociateCommand request,
        RequestHandlerDelegate<ErrorOr<AssociateResponse>> next, CancellationToken cancellationToken)
    {
        var (success, errors) = Validate(request.Associate);
        if (!success)
            return errors;
        
        var associateId = new AssociateId(request.Id);
        var existing = await _reader.PickAsync(associateId, cancellationToken);

        if (existing is null)
            return Errors.Associates.NotFound;

        request.ToMutate = existing;

        var response = await next();
        return response;
    }
}