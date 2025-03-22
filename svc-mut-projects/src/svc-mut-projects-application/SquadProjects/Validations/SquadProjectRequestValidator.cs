namespace DistribuTe.Mutators.Projects.Application.SquadProjects.Validations;

using DataContracts;
using FluentValidation;

public class SquadProjectRequestValidator : AbstractValidator<SquadProjectRequest>
{
    public SquadProjectRequestValidator()
    {
        RuleFor(a => a.Squad_Id).GreaterThan(0);
        RuleFor(a => a.Project_Id).GreaterThan(0);
        RuleFor(a => a.Started_On).NotEmpty();
        RuleFor(a => a.Ended_On)
            .GreaterThan(a => a.Started_On)
            .When(a => a.Ended_On.HasValue);
    }
}