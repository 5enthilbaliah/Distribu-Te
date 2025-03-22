namespace DistribuTe.Mutators.Projects.Application.ProjectCategories.Validations;

using Domain.Entities;
using Domain.Errors;
using ErrorOr;
using Framework.AppEssentials;
using MediatR;

public class TrashProjectCategoryCommandValidationBehavior(IProjectsReader<ProjectCategory, ProjectCategoryId> reader,
    IExistingEntityMarker<ProjectCategory, ProjectCategoryId> entityMarker) : 
    IPipelineBehavior<TrashProjectCategoryCommand, ErrorOr<bool>>
{
    private readonly IProjectsReader<ProjectCategory, ProjectCategoryId> _reader = 
        reader ?? throw new ArgumentNullException(nameof(reader));
    private readonly IExistingEntityMarker<ProjectCategory, ProjectCategoryId> _entityMarker = 
        entityMarker ?? throw new ArgumentNullException(nameof(entityMarker));
    
    public async Task<ErrorOr<bool>> Handle(TrashProjectCategoryCommand request, 
        RequestHandlerDelegate<ErrorOr<bool>> next, CancellationToken cancellationToken)
    {
        var projectCategoryId = new ProjectCategoryId(request.Id);
        var existing = await _reader.PickAsync(projectCategoryId, cancellationToken);
        
        if (existing is null)
            return Errors.ProjectCategories.NotFound;
        
        _entityMarker.Id = projectCategoryId;
        _entityMarker.Entity = existing;
        
        return await next();
    }
}