namespace DistribuTe.Framework.AppEssentials.Validations;

using ErrorOr;
using FluentValidation.Results;

public static class ValidationExtensions
{
    public static (bool Success, List<Error> Errors) AsErrorOrResult(this ValidationResult validationResult)
    {
        if (validationResult.IsValid) return (true, []);
        
        var errors = validationResult.Errors
            .ConvertAll(e => Error.Validation(e.PropertyName, e.ErrorMessage));
        return (false, errors);

    }
}