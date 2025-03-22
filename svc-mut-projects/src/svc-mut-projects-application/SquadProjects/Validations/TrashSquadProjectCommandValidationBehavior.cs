namespace DistribuTe.Mutators.Projects.Application.SquadProjects.Validations;

using Domain.Entities;
using Domain.Errors;
using ErrorOr;
using Framework.AppEssentials;
using MediatR;

public class TrashSquadProjectCommandValidationBehavior(IEntityReader<SquadProject, SquadProjectId> reader,
    IExistingEntityMarker<SquadProject, SquadProjectId> entityMarker) : 
    IPipelineBehavior<TrashSquadProjectCommand, ErrorOr<bool>>
{
    private readonly IEntityReader<SquadProject, SquadProjectId> _reader = 
        reader ?? throw new ArgumentNullException(nameof(reader));
    private readonly IExistingEntityMarker<SquadProject, SquadProjectId> _entityMarker = 
        entityMarker ?? throw new ArgumentNullException(nameof(entityMarker));
    
    public async Task<ErrorOr<bool>> Handle(TrashSquadProjectCommand request, 
        RequestHandlerDelegate<ErrorOr<bool>> next, CancellationToken cancellationToken)
    {
        var squadId = new SquadId(request.SquadId);
        var projectId = new ProjectId(request.ProjectId);
        var squadProjectId = new SquadProjectId(squadId, projectId);
        var existing = await _reader.PickAsync(squadProjectId, cancellationToken);
        
        if (existing is null)
            return Errors.SquadProjects.NotFound;
        
        _entityMarker.Id = squadProjectId;
        _entityMarker.Entity = existing;
        
        return await next();
    }
}