namespace DistribuTe.Mutators.Teams.Application.Squads.Validations;

using DataContracts;
using ErrorOr;
using FluentValidation;
using Shared;

public abstract class SquadRequestValidationBehavior(IValidator<SquadRequest> validator)
{
    private readonly IValidator<SquadRequest> _validator =
        validator ?? throw new ArgumentNullException(nameof(validator));

    protected (bool Success, List<Error> Errors) Validate(SquadRequest request)
        => _validator.Validate(request).AsErrorOrResult();
}