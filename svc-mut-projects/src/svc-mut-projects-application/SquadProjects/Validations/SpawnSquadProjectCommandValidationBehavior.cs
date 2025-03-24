namespace DistribuTe.Mutators.Projects.Application.SquadProjects.Validations;

using DataContracts;
using Domain.Entities;
using Domain.Errors;
using ErrorOr;
using FluentValidation;
using Framework.AppEssentials;
using Framework.AppEssentials.Validations;
using MediatR;

public class SpawnSquadProjectCommandValidationBehavior(IEntityReader<SquadProject, SquadProjectId> reader,
    IValidator<SquadProjectRequest> validator, IEntityReader<Project, ProjectId> entityReader,
    ITeamsAggregateApiReader teamsAggregateApiReader) : 
    DistribuTeRequestValidationBehavior<SquadProjectRequest>(validator),
    IPipelineBehavior<SpawnSquadProjectCommand, ErrorOr<SquadProjectResponse>>
{
    private readonly IEntityReader<SquadProject, SquadProjectId> _reader = 
        reader ?? throw new ArgumentNullException(nameof(reader));
    private readonly IEntityReader<Project, ProjectId> _entityReader = 
        entityReader ?? throw new ArgumentNullException(nameof(entityReader));
    private readonly ITeamsAggregateApiReader _teamsAggregateApiReader = 
        teamsAggregateApiReader ?? throw new ArgumentNullException(nameof(teamsAggregateApiReader));
    
    public async Task<ErrorOr<SquadProjectResponse>> Handle(SpawnSquadProjectCommand request, 
        RequestHandlerDelegate<ErrorOr<SquadProjectResponse>> next, CancellationToken cancellationToken)
    {
        var (success, errors) = Validate(request.SquadProject);
        if (!success)
            return errors;
        var squadId = new SquadId(request.SquadProject.Squad_Id);
        var projectId = new ProjectId(request.SquadProject.Project_Id);
        
        var projectFound = await _entityReader.AnyAsync(a => a.Id == projectId,
            cancellationToken);
        if (!projectFound)
            return Errors.Projects.NotFound;
        
        if (request.Token == null)
            return Error.Validation("project.token", "Token not provided");
        
        var squadFound = await _teamsAggregateApiReader.SquadExistsAsync(squadId, request.Token,
            cancellationToken);
        if (!squadFound)
            return Errors.Squads.NotFound;
        
        var allocationFound = await _reader.AnyAsync(a => a.SquadId == squadId
                                                          && a.ProjectId == projectId, cancellationToken);
        if (allocationFound)
            return Errors.SquadProjects.DuplicateAllocation;
        
        var response = await next();
        return response;
    }
}