namespace DistribuTe.Mutators.Projects.Apis.Controllers;

using System.Net;
using Application.SquadProjects;
using Application.SquadProjects.DataContracts;
using Asp.Versioning;
using ErrorOr;
using Framework.ApiEssentials.Odata.Controllers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using ApiErrors = DistribuTe.Mutators.Projects.Apis.Controllers.Errors.Errors;

[Route("protected/squad-projects")]
[ApiVersion("1.0")]
[Produces("application/json")]
[Authorize(Roles = "mutate-projects")]
public class SquadProjectController(IMediator mediator) : DistribuTeController
{
    private readonly IMediator _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    
    [Route("")]
    [HttpPost]
    [ProducesResponseType(typeof(SquadProjectResponse), (int)HttpStatusCode.Created)]
    [EnableQuery()]
    public async Task<IActionResult> SpawnAsync([FromBody] SquadProjectRequest squadAssociate, CancellationToken ct = default)
    {
        var result = await _mediator.Send(new SpawnSquadProjectCommand
        {
            SquadProject = squadAssociate,
        }, ct).ConfigureAwait(false);
        
        return result.Match(
            validResult => StatusCode((int)HttpStatusCode.Created, validResult),
            Problem
        );
    }
    
    [Route("{squadId:int}-{projectId:int}")]
    [HttpPut]
    [ProducesResponseType(typeof(SquadProjectResponse), (int)HttpStatusCode.OK)]
    [EnableQuery()]
    public async Task<IActionResult> CommitAsync(int squadId, int projectId, [FromBody] SquadProjectRequest squadAssociate, 
        CancellationToken ct = default)
    {
        var errors = new List<Error>();
        if (squadId != squadAssociate.Squad_Id)
            errors.Add(ApiErrors.SquadProjectEndpoints.MismatchSquadId);
        if (projectId != squadAssociate.Project_Id)
            errors.Add(ApiErrors.SquadProjectEndpoints.MismatchProjectId);
        
        if (errors.Count != 0)
            return Problem(errors);
        
        var result = await _mediator.Send(new CommitSquadProjectCommand
        {
            SquadProject = squadAssociate,
            ProjectId = projectId,
            SquadId = squadId,
        }, ct).ConfigureAwait(false);
        
        return result.Match(
            Ok,
            Problem
        );
    }
    
    [Route("{squadId:int}-{projectId:int}")]
    [HttpDelete]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> TrashAsync(int squadId, int projectId, CancellationToken ct = default)
    {
        var result = await _mediator.Send(new TrashSquadProjectCommand
        {
            SquadId = squadId,
            ProjectId = projectId,
        }, ct).ConfigureAwait(false);
        
        return result.Match(
            _ => NoContent(),
            Problem
        );
    }
}