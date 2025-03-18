namespace DistribuTe.Aggregates.Teams.Apis.Controllers;

using System.Net;
using Application.Squads;
using Application.Squads.DataContracts;
using Asp.Versioning;
using Framework.ApiEssentials.Odata;
using Framework.ApiEssentials.Odata.Controllers;
using Framework.ApiEssentials.Odata.Implementations;
using Framework.AppEssentials;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

[Route("protected/squads")]
[ApiVersion("1.0")]
[Produces("application/json")]
public class SquadQueryController(ISender sender, OdataFilterVisitor visitor,
    IOdataNavigator<SquadModel> navigator, IRequestContext requestContext) : 
    DistribuTeQueryController<SquadModel>(visitor, navigator, requestContext)
{
    private readonly ISender _sender = sender ?? throw new ArgumentNullException(nameof(sender));
    
    [HttpGet]
    [Route("")]
    [ProducesResponseType(typeof(IList<SquadModel>), (int)HttpStatusCode.OK)]
    [EnableQuery()]
    public async Task<IActionResult> SearchAsync(ODataQueryOptions<SquadModel> queryOptions,
        CancellationToken cancellationToken = default)
    {
        var result = await _sender.Send(new YieldSquadsQuery
        {
            LinqQueryFacade = GenerateWhereClauseFacadeFrom(queryOptions)
        }, cancellationToken);
        
        return result.Match(
            Ok,
            Problem
        );
    }

    [Route("{id:int}")]
    [HttpGet]
    [ProducesResponseType(typeof(SquadModel), (int)HttpStatusCode.OK)]
    [EnableQuery()]
    public async Task<IActionResult> GetByIdAsync(int id, ODataQueryOptions<SquadModel> queryOptions,
        CancellationToken cancellationToken = default)
    {
        var result = await _sender.Send(new PickSquadQuery
        {
            LinqQueryFacade = GenerateWhereClauseFacadeFrom(queryOptions),
            Id = id
        }, cancellationToken);
        
        return result.Match(
            Ok,
            Problem
        );
    }
}