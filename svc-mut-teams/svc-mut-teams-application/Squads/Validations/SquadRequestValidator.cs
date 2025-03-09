namespace DistribuTe.Mutators.Teams.Application.Squads.Validations;

using DataContracts;
using FluentValidation;

public class SquadRequestValidator : AbstractValidator<SquadRequest>
{
    public SquadRequestValidator()
    {
        RuleFor(a => a.Name).NotEmpty();
        RuleFor(a => a.Name).MaximumLength(45);
        
        RuleFor(a => a.Code).NotEmpty();
        RuleFor(a => a.Code).MaximumLength(20);
        
        RuleFor(a => a.Description).MaximumLength(200);
    }
}