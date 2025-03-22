namespace DistribuTe.Mutators.Projects.Application.ProjectCategories.Validations;

using DataContracts;
using Domain.Entities;
using Domain.Errors;
using ErrorOr;
using FluentValidation;
using Framework.AppEssentials;
using Framework.AppEssentials.Validations;
using MediatR;

public class CommitProjectCategoryCommandValidationBehavior(IEntityReader<ProjectCategory, ProjectCategoryId> reader,
    IValidator<ProjectCategoryRequest> validator, IExistingEntityMarker<ProjectCategory, ProjectCategoryId> entityMarker) : 
    DistribuTeRequestValidationBehavior<ProjectCategoryRequest>(validator),
    IPipelineBehavior<CommitProjectCategoryCommand, ErrorOr<ProjectCategoryResponse>>
{
    private readonly IEntityReader<ProjectCategory, ProjectCategoryId> _reader =
        reader ?? throw new ArgumentNullException(nameof(reader));
    private readonly IExistingEntityMarker<ProjectCategory, ProjectCategoryId> _entityMarker = 
        entityMarker ?? throw new ArgumentNullException(nameof(entityMarker));


    public async Task<ErrorOr<ProjectCategoryResponse>> Handle(CommitProjectCategoryCommand request,
        RequestHandlerDelegate<ErrorOr<ProjectCategoryResponse>> next, CancellationToken cancellationToken)
    {
        var (success, errors) = Validate(request.ProjectCategory);
        if (!success)
            return errors;
        
        var projectCategoryId = new ProjectCategoryId(request.Id);
        var existing = await _reader.PickAsync(projectCategoryId, cancellationToken);

        if (existing is null)
            return Errors.ProjectCategories.NotFound;

        _entityMarker.Id = projectCategoryId;
        _entityMarker.Entity = existing;

        var response = await next();
        return response;
    }
}