namespace DistribuTe.Mutators.Teams.Application.SquadAssociates.Validations;

using DataContracts;
using FluentValidation;

public class SquadAssociateRequestValidator : AbstractValidator<SquadAssociateRequest>
{
    public SquadAssociateRequestValidator()
    {
        RuleFor(a => a.Squad_Id).GreaterThan(0);
        RuleFor(a => a.Associate_Id).GreaterThan(0);
        RuleFor(a => a.Capacity).GreaterThanOrEqualTo(0);
        RuleFor(a => a.Started_On).NotEmpty();
        RuleFor(a => a.Ended_On)
            .GreaterThan(a => a.Started_On)
            .When(a => a.Ended_On.HasValue);
    }
}