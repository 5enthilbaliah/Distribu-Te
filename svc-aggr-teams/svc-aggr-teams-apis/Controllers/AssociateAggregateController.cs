namespace DistribuTe.Aggregates.Teams.Apis.Controllers;

using Asp.Versioning;
using Framework.OData.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

[Route("protected/aggregates/associates")]
[ApiVersion("1.0")]
[Produces("application/json")]
public class AssociateAggregateController(IMediator mediator) : DistribuTeController
{
    private readonly IMediator _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    
    
}