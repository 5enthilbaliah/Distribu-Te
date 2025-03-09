namespace DistribuTe.Mutators.Teams.Application.Shared;

using ErrorOr;
using FluentValidation;

public abstract class DistribuTeRequestValidationBehavior<TRequest>(IValidator<TRequest> validator)
    where TRequest : class, new()
{
    private readonly IValidator<TRequest> _validator =
        validator ?? throw new ArgumentNullException(nameof(validator));

    protected (bool Success, List<Error> Errors) Validate(TRequest request)
        => _validator.Validate(request).AsErrorOrResult();
}