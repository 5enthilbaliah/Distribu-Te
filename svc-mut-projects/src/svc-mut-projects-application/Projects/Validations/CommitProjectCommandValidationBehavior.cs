namespace DistribuTe.Mutators.Projects.Application.Projects.Validations;

using DataContracts;
using Domain.Entities;
using Domain.Errors;
using ErrorOr;
using FluentValidation;
using Framework.AppEssentials;
using Framework.AppEssentials.Validations;
using MediatR;

public class CommitProjectCommandValidationBehavior(IEntityReader<Project, ProjectId> reader,
    IEntityReader<ProjectCategory, ProjectCategoryId> projectCategoryReader,
    IValidator<ProjectRequest> validator, IExistingEntityMarker<Project, ProjectId> entityMarker) : 
    DistribuTeRequestValidationBehavior<ProjectRequest>(validator),
    IPipelineBehavior<CommitProjectCommand, ErrorOr<ProjectResponse>>
{
    private readonly IEntityReader<Project, ProjectId> _reader =
        reader ?? throw new ArgumentNullException(nameof(reader));
    private readonly IEntityReader<ProjectCategory, ProjectCategoryId> _projectCategoryReader =
        projectCategoryReader ?? throw new ArgumentNullException(nameof(projectCategoryReader));
    private readonly IExistingEntityMarker<Project, ProjectId> _entityMarker = 
        entityMarker ?? throw new ArgumentNullException(nameof(entityMarker));


    public async Task<ErrorOr<ProjectResponse>> Handle(CommitProjectCommand request,
        RequestHandlerDelegate<ErrorOr<ProjectResponse>> next, CancellationToken cancellationToken)
    {
        var (success, errors) = Validate(request.Project);
        if (!success)
            return errors;
        
        var projectId = new ProjectId(request.Id);
        var existing = await _reader.PickAsync(projectId, cancellationToken);
        if (existing is null)
            return Errors.Projects.NotFound;
        
        var categoryId = new ProjectCategoryId(request.Project.Category_Id);
        var categoryFound = await _projectCategoryReader.AnyAsync(a => a.Id == categoryId, 
            cancellationToken);
        if (!categoryFound)
            return Errors.ProjectCategories.NotFound;
        
        var codeFound = await _reader.AnyAsync(a => a.Id != projectId && a.Code == request.Project.Code,
            cancellationToken);
        if (codeFound)
            return Errors.Projects.DuplicateCode;
        
        var nameFound = await _reader.AnyAsync(a => a.Id != projectId && a.Name == request.Project.Name,
            cancellationToken);
        if (nameFound)
            return Errors.Projects.DuplicateName;

        _entityMarker.Id = projectId;
        _entityMarker.Entity = existing;

        var response = await next();
        return response;
    }
}