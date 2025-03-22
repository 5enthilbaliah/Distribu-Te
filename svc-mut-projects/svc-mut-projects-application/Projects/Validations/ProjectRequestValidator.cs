namespace DistribuTe.Mutators.Projects.Application.Projects.Validations;

using DataContracts;
using FluentValidation;

public class ProjectRequestValidator : AbstractValidator<ProjectRequest>
{
    public ProjectRequestValidator()
    {
        RuleFor(a => a.Name).NotEmpty();
        RuleFor(a => a.Name).MaximumLength(45);
        
        RuleFor(a => a.Code).NotEmpty();
        RuleFor(a => a.Code).MaximumLength(20);
        
        RuleFor(a => a.Description).MaximumLength(200);
    }
}