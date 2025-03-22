namespace DistribuTe.Mutators.Projects.Application.SquadProjects.Validations;

using DataContracts;
using Domain.Entities;
using Domain.Errors;
using ErrorOr;
using FluentValidation;
using Framework.AppEssentials;
using Framework.AppEssentials.Validations;
using MediatR;

public class CommitSquadProjectCommandValidationBehavior(IEntityReader<SquadProject, SquadProjectId> reader,
    IValidator<SquadProjectRequest> validator, IExistingEntityMarker<SquadProject, SquadProjectId> entityMarker) : 
    DistribuTeRequestValidationBehavior<SquadProjectRequest>(validator),
    IPipelineBehavior<CommitSquadProjectCommand, ErrorOr<SquadProjectResponse>>
{
    private readonly IEntityReader<SquadProject, SquadProjectId> _reader =
        reader ?? throw new ArgumentNullException(nameof(reader));
    private readonly IExistingEntityMarker<SquadProject, SquadProjectId> _entityMarker = 
        entityMarker ?? throw new ArgumentNullException(nameof(entityMarker));


    public async Task<ErrorOr<SquadProjectResponse>> Handle(CommitSquadProjectCommand request,
        RequestHandlerDelegate<ErrorOr<SquadProjectResponse>> next, CancellationToken cancellationToken)
    {
        var (success, errors) = Validate(request.SquadProject);
        if (!success)
            return errors;
        
        var squadId = new SquadId(request.SquadId);
        var projectId = new ProjectId(request.ProjectId);
        var squadProjectId = new SquadProjectId(squadId, projectId);
        var existing = await _reader.PickAsync(squadProjectId, cancellationToken);

        if (existing is null)
            return Errors.SquadProjects.NotFound;

        _entityMarker.Id = squadProjectId;
        _entityMarker.Entity = existing;

        var response = await next();
        return response;
    }
}