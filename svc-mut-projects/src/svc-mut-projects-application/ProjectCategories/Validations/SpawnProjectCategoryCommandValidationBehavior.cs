namespace DistribuTe.Mutators.Projects.Application.ProjectCategories.Validations;

using DataContracts;
using Domain.Entities;
using Domain.Errors;
using ErrorOr;
using FluentValidation;
using Framework.AppEssentials;
using Framework.AppEssentials.Validations;
using MediatR;

public class SpawnProjectCategoryCommandValidationBehavior(IEntityReader<ProjectCategory, ProjectCategoryId> reader,
    IValidator<ProjectCategoryRequest> validator) : DistribuTeRequestValidationBehavior<ProjectCategoryRequest>(validator),
    IPipelineBehavior<SpawnProjectCategoryCommand, ErrorOr<ProjectCategoryResponse>>
{
    private readonly IEntityReader<ProjectCategory, ProjectCategoryId> _reader = 
        reader ?? throw new ArgumentNullException(nameof(reader));
    
    public async Task<ErrorOr<ProjectCategoryResponse>> Handle(SpawnProjectCategoryCommand request, 
        RequestHandlerDelegate<ErrorOr<ProjectCategoryResponse>> next, CancellationToken cancellationToken)
    {
        var (success, errors) = Validate(request.ProjectCategory);
        if (!success)
            return errors;
        
        var codeFound = await _reader.AnyAsync(a => a.Code == request.ProjectCategory.Code,
            cancellationToken);
        if (codeFound)
            return Errors.ProjectCategories.DuplicateCode;
        
        var nameFound = await _reader.AnyAsync(a => a.Name == request.ProjectCategory.Name,
            cancellationToken);
        if (nameFound)
            return Errors.ProjectCategories.DuplicateName;
        
        var response = await next();
        return response;
    }
}