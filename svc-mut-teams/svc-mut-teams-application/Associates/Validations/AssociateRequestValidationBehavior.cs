namespace DistribuTe.Mutators.Teams.Application.Associates.Validations;

using DataContracts;
using ErrorOr;
using FluentValidation;
using Shared;

public abstract class AssociateRequestValidationBehavior(IValidator<AssociateRequest> validator)
{
    private readonly IValidator<AssociateRequest> _validator =
        validator ?? throw new ArgumentNullException(nameof(validator));

    protected (bool Success, List<Error> Errors) Validate(AssociateRequest request)
        => _validator.Validate(request).AsErrorOrResult();
}