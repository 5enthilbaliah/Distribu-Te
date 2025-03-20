namespace DistribuTe.Aggregates.Teams.Apis.Controllers;

using System.Net;
using Application.Associates;
using Application.Associates.DataContracts;
using Asp.Versioning;
using Framework.ApiEssentials.Odata;
using Framework.ApiEssentials.Odata.Controllers;
using Framework.ApiEssentials.Odata.Filters;
using Framework.ApiEssentials.Odata.Implementations;
using Framework.AppEssentials;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

[Route("protected/associates")]
[ApiVersion("1.0")]
[Produces("application/json")]
public class AssociateQueryController(ISender sender, OdataFilterVisitor visitor,
    IOdataNavigator<AssociateModel> navigator, IRequestContext requestContext) : 
    DistribuTeQueryController<AssociateModel>(visitor, navigator, requestContext)
{
    private readonly ISender _sender = sender ?? throw new ArgumentNullException(nameof(sender));

    [HttpGet]
    [Route("")]
    [ProducesResponseType(typeof(IList<AssociateModel>), (int)HttpStatusCode.OK)]
    [DistribuTeEnableQuery()]
    public async Task<IActionResult> SearchAsync(ODataQueryOptions<AssociateModel> queryOptions,
        CancellationToken cancellationToken = default)
    {
        var result = await _sender.Send(new YieldAssociatesQuery
        {
            EntityLinqFacade = GenerateWhereClauseFacadeFrom(queryOptions)
        }, cancellationToken);
        
        return result.Match(
            Ok,
            Problem
        );
    }

    [Route("{id:int}")]
    [HttpGet]
    [ProducesResponseType(typeof(AssociateModel), (int)HttpStatusCode.OK)]
    [DistribuTeEnableQuery()]
    public async Task<IActionResult> GetByIdAsync(int id, ODataQueryOptions<AssociateModel> queryOptions,
        CancellationToken cancellationToken = default)
    {
        var result = await _sender.Send(new PickAssociateQuery
        {
            EntityLinqFacade = GenerateWhereClauseFacadeFrom(queryOptions),
            Id = id
        }, cancellationToken);
        
        return result.Match(
            Ok,
            Problem
        );
    }
}