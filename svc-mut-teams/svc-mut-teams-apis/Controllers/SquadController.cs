namespace DistribuTe.Mutators.Teams.Apis.Controllers;

using System.Net;
using Application.Squads;
using Application.Squads.Models;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("protected/squads")]
[ApiVersion("1.0")]
[Produces("application/json")]
public class SquadController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    
    [Route("")]
    [HttpPost]
    [ProducesResponseType(typeof(SquadVm), (int)HttpStatusCode.Created)]
    public async Task<IActionResult> SpawnAsync([FromBody] SquadRm squad, CancellationToken ct = default)
    {
        var result = await _mediator.Send(new SpawnSquadCommand
        {
            Squad = squad
        }, ct).ConfigureAwait(false);
        
        return StatusCode((int)HttpStatusCode.Created, result);
    }
    
    [Route("{id:int}")]
    [HttpPut]
    [ProducesResponseType(typeof(SquadVm), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> CommitAsync(int id, [FromBody] SquadRm squad, CancellationToken ct = default)
    {
        var result = await _mediator.Send(new CommitSquadCommand
        {
            Squad = squad,
            Id = id,
        }, ct).ConfigureAwait(false);
        
        return Ok(result);
    }
    
    [Route("{id:int}")]
    [HttpDelete]
    [ProducesResponseType(typeof(SquadVm), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> TrashAsync(int id, CancellationToken ct = default)
    {
        var result = await _mediator.Send(new TrashSquadCommand
        {
            Id = id,
        }, ct).ConfigureAwait(false);
        
        return Ok(result);
    }
}