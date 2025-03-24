namespace DistribuTe.Mutators.Projects.Apis.Controllers;

using System.Net;
using Application.Projects;
using Application.Projects.DataContracts;
using Asp.Versioning;
using Framework.ApiEssentials.Odata.Controllers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

[Route("protected/projects")]
[ApiVersion("1.0")]
[Produces("application/json")]
[Authorize(Roles = "mutate-projects")]
public class ProjectController(IMediator mediator) : DistribuTeController
{
    private readonly IMediator _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    
    [Route("")]
    [HttpPost]
    [ProducesResponseType(typeof(ProjectResponse), (int)HttpStatusCode.Created)]
    [EnableQuery()]
    public async Task<IActionResult> SpawnAsync([FromBody] ProjectRequest project, CancellationToken ct = default)
    {
        var result = await _mediator.Send(new SpawnProjectCommand
        {
            Project = project,
        }, ct).ConfigureAwait(false);

        return result.Match(
            validResult => StatusCode((int)HttpStatusCode.Created, validResult),
            Problem
        );
    }

    [Route("{id:int}")]
    [HttpPut]
    [ProducesResponseType(typeof(ProjectResponse), (int)HttpStatusCode.OK)]
    [EnableQuery()]
    public async Task<IActionResult> CommitAsync(int id, [FromBody] ProjectRequest project, CancellationToken ct = default)
    {
        var result = await _mediator.Send(new CommitProjectCommand
        {
            Project = project,
            Id = id,
        }, ct).ConfigureAwait(false);
        
        return result.Match(
            Ok,
            Problem
        );
    }
    
    [Route("{id:int}")]
    [HttpDelete]
    [ProducesResponseType(typeof(ProjectResponse), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> TrashAsync(int id, CancellationToken ct = default)
    {
        var result = await _mediator.Send(new TrashProjectCommand
        {
            Id = id,
        }, ct).ConfigureAwait(false);
        
        return result.Match(
            _ => NoContent(),
            Problem
        );
    }
}