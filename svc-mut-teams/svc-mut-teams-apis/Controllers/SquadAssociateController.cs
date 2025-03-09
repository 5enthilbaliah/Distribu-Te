namespace DistribuTe.Mutators.Teams.Apis.Controllers;

using System.Net;
using Application.SquadAssociates;
using Application.SquadAssociates.DataContracts;
using Asp.Versioning;
using ErrorOr;
using Framework.OData.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

[Route("protected/squad-associates")]
[ApiVersion("1.0")]
[Produces("application/json")]
public class SquadAssociateController(IMediator mediator) : DistribuTeController
{
    private readonly IMediator _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    
    [Route("")]
    [HttpPost]
    [ProducesResponseType(typeof(SquadAssociateResponse), (int)HttpStatusCode.Created)]
    [EnableQuery()]
    public async Task<IActionResult> SpawnAsync([FromBody] SquadAssociateRequest squadAssociate, CancellationToken ct = default)
    {
        var result = await _mediator.Send(new SpawnSquadAssociateCommand
        {
            SquadAssociate = squadAssociate,
        }, ct).ConfigureAwait(false);
        
        return result.Match(
            validResult => StatusCode((int)HttpStatusCode.Created, validResult),
            Problem
        );
    }
    
    [Route("{squadId:int}-{associateId:int}")]
    [HttpPut]
    [ProducesResponseType(typeof(SquadAssociateResponse), (int)HttpStatusCode.OK)]
    [EnableQuery()]
    public async Task<IActionResult> CommitAsync(int squadId, int associateId, [FromBody] SquadAssociateRequest squadAssociate, 
        CancellationToken ct = default)
    {
        var errors = new List<Error>();
        if (squadId != squadAssociate.Squad_Id)
            errors.Add(Errors.Errors.SquadAssociateEndpoints.MismatchSquadId);
        if (associateId != squadAssociate.Associate_Id)
            errors.Add(Errors.Errors.SquadAssociateEndpoints.MismatchAssociateId);
        
        if (errors.Count != 0)
            return Problem(errors);
        
        var result = await _mediator.Send(new CommitSquadAssociateCommand
        {
            SquadAssociate = squadAssociate,
            AssociateId = associateId,
            SquadId = squadId,
        }, ct).ConfigureAwait(false);
        
        return result.Match(
            Ok,
            Problem
        );
    }
    
    [Route("{squadId:int}-{associateId:int}")]
    [HttpDelete]
    [ProducesResponseType(typeof(SquadAssociateResponse), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> TrashAsync(int squadId, int associateId, CancellationToken ct = default)
    {
        var result = await _mediator.Send(new TrashSquadAssociateCommand
        {
            SquadId = squadId,
            AssociateId = associateId,
        }, ct).ConfigureAwait(false);
        
        return result.Match(
            _ => NoContent(),
            Problem
        );
    }
}