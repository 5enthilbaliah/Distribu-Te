namespace DistribuTe.Mutators.Projects.Application.Projects.Validations;

using Domain.Entities;
using Domain.Errors;
using ErrorOr;
using Framework.AppEssentials;
using MediatR;

public class TrashProjectCommandValidationBehavior(IProjectsReader<Project, ProjectId> reader,
    IExistingEntityMarker<Project, ProjectId> entityMarker) : 
    IPipelineBehavior<TrashProjectCommand, ErrorOr<bool>>
{
    private readonly IProjectsReader<Project, ProjectId> _reader = 
        reader ?? throw new ArgumentNullException(nameof(reader));
    private readonly IExistingEntityMarker<Project, ProjectId> _entityMarker = 
        entityMarker ?? throw new ArgumentNullException(nameof(entityMarker));
    
    public async Task<ErrorOr<bool>> Handle(TrashProjectCommand request, 
        RequestHandlerDelegate<ErrorOr<bool>> next, CancellationToken cancellationToken)
    {
        var projectId = new ProjectId(request.Id);
        var existing = await _reader.PickAsync(projectId, cancellationToken);
        
        if (existing is null)
            return Errors.Projects.NotFound;
        
        _entityMarker.Id = projectId;
        _entityMarker.Entity = existing;
        
        return await next();
    }
}