namespace DistribuTe.Mutators.Teams.Apis.Controllers;

using System.Net;
using Application.Squads;
using Application.Squads.DataContracts;
using Asp.Versioning;
using Framework.OData.Attributes;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

[ApiController]
[Route("protected/squads")]
[ApiVersion("1.0")]
[Produces("application/json")]
public class SquadController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    
    [Route("")]
    [HttpPost]
    [ProducesResponseType(typeof(SquadResponse), (int)HttpStatusCode.Created)]
    [EnableQuery()]
    public async Task<IActionResult> SpawnAsync([FromBody] SquadRequest squad, CancellationToken ct = default)
    {
        var result = await _mediator.Send(new SpawnSquadCommand
        {
            Squad = squad
        }, ct).ConfigureAwait(false);
        
        return StatusCode((int)HttpStatusCode.Created, result);
    }
    
    [Route("{id:int}")]
    [HttpPut]
    [ProducesResponseType(typeof(SquadResponse), (int)HttpStatusCode.OK)]
    [EnableQuery()]
    public async Task<IActionResult> CommitAsync(int id, [FromBody] SquadRequest squad, CancellationToken ct = default)
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
    [ProducesResponseType(typeof(SquadResponse), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> TrashAsync(int id, CancellationToken ct = default)
    {
        var result = await _mediator.Send(new TrashSquadCommand
        {
            Id = id,
        }, ct).ConfigureAwait(false);
        
        return Ok(result);
    }
}