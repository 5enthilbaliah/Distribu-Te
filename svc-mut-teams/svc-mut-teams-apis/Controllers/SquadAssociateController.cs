namespace DistribuTe.Mutators.Teams.Apis.Controllers;

using System.Net;
using Application.SquadAssociates;
using Application.SquadAssociates.Models;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("protected/squad-associates")]
[ApiVersion("1.0")]
[Produces("application/json")]
public class SquadAssociateController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    
    [Route("")]
    [HttpPost]
    [ProducesResponseType(typeof(SquadAssociateVm), (int)HttpStatusCode.Created)]
    public async Task<IActionResult> SpawnAsync([FromBody] SquadAssociateRm squadAssociate, CancellationToken ct = default)
    {
        var result = await _mediator.Send(new SpawnSquadAssociateCommand
        {
            SquadAssociate = squadAssociate,
        }, ct).ConfigureAwait(false);
        
        return StatusCode((int)HttpStatusCode.Created, result);
    }
    
    [Route("{squadId:int}-{associateId:int}")]
    [HttpPut]
    [ProducesResponseType(typeof(SquadAssociateVm), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> CommitAsync(int squadId, int associateId, [FromBody] SquadAssociateRm squadAssociate, 
        CancellationToken ct = default)
    {
        var result = await _mediator.Send(new CommitSquadAssociateCommand
        {
            SquadAssociate = squadAssociate,
            AssociateId = associateId,
            SquadId = squadId,
        }, ct).ConfigureAwait(false);
        
        return Ok(result);
    }
    
    [Route("{squadId:int}-{associateId:int}")]
    [HttpDelete]
    [ProducesResponseType(typeof(SquadAssociateVm), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> TrashAsync(int squadId, int associateId, CancellationToken ct = default)
    {
        var result = await _mediator.Send(new TrashSquadAssociateCommand
        {
            SquadId = squadId,
            AssociateId = associateId,
        }, ct).ConfigureAwait(false);
        
        return Ok(result);
    }
}