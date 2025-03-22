namespace DistribuTe.Mutators.Projects.Application.ProjectCategories.Validations;

using DataContracts;
using FluentValidation;

public class ProjectCategoryRequestValidator : AbstractValidator<ProjectCategoryRequest>
{ 
    public ProjectCategoryRequestValidator()
    {
        RuleFor(a => a.Name).NotEmpty();
        RuleFor(a => a.Name).MaximumLength(45);
        
        RuleFor(a => a.Code).NotEmpty();
        RuleFor(a => a.Code).MaximumLength(20);
        
        RuleFor(a => a.Description).MaximumLength(200);
    }
}