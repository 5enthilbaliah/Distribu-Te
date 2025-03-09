namespace DistribuTe.Mutators.Teams.Apis.Controllers;

using System.Net;
using Application.Associates;
using Application.Associates.DataContracts;
using Asp.Versioning;
using Framework.OData.Attributes;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

[Route("protected/associates")]
[ApiVersion("1.0")]
[Produces("application/json")]
public class AssociateController(IMediator mediator) : DistribuTeController
{
    private readonly IMediator _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    
    [Route("")]
    [HttpPost]
    [ProducesResponseType(typeof(AssociateResponse), (int)HttpStatusCode.Created)]
    [EnableQuery()]
    public async Task<IActionResult> SpawnAsync([FromBody] AssociateRequest associate, CancellationToken ct = default)
    {
        var result = await _mediator.Send(new SpawnAssociateCommand
        {
            Associate = associate,
        }, ct).ConfigureAwait(false);

        return result.Match(
            validResult => StatusCode((int)HttpStatusCode.Created, validResult),
            Problem
        );
    }

    [Route("{id:int}")]
    [HttpPut]
    [ProducesResponseType(typeof(AssociateResponse), (int)HttpStatusCode.OK)]
    [EnableQuery()]
    public async Task<IActionResult> CommitAsync(int id, [FromBody] AssociateRequest associate, CancellationToken ct = default)
    {
        var result = await _mediator.Send(new CommitAssociateCommand
        {
            Associate = associate,
            Id = id,
        }, ct).ConfigureAwait(false);
        
        return result.Match(
            Ok,
            Problem
        );
    }
    
    [Route("{id:int}")]
    [HttpDelete]
    [ProducesResponseType(typeof(AssociateResponse), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> TrashAsync(int id, CancellationToken ct = default)
    {
        var result = await _mediator.Send(new TrashAssociateCommand
        {
            Id = id,
        }, ct).ConfigureAwait(false);
        
        return result.Match(
            validResult => Ok(validResult),
            Problem
        );
    }
}