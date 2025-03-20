namespace DistribuTe.Aggregates.Teams.Apis.Controllers;

using System.Net;
using Application.SquadAssociates;
using Application.SquadAssociates.DataContracts;
using Application.Squads.DataContracts;
using Asp.Versioning;
using Framework.ApiEssentials.Odata;
using Framework.ApiEssentials.Odata.Controllers;
using Framework.ApiEssentials.Odata.Filters;
using Framework.ApiEssentials.Odata.Implementations;
using Framework.AppEssentials;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

[Route("protected/squad-associates")]
[ApiVersion("1.0")]
[Produces("application/json")]
public class SquadAssociateQueryController(ISender sender, OdataFilterVisitor visitor,
    IOdataNavigator<SquadAssociateModel> navigator, IRequestContext requestContext) : 
    DistribuTeQueryController<SquadAssociateModel>(visitor, navigator, requestContext)
{
    private readonly ISender _sender = sender ?? throw new ArgumentNullException(nameof(sender));
    
    [HttpGet]
    [Route("")]
    [ProducesResponseType(typeof(IList<SquadAssociateModel>), (int)HttpStatusCode.OK)]
    [DistribuTeEnableQuery()]
    public async Task<IActionResult> SearchAsync(ODataQueryOptions<SquadAssociateModel> queryOptions,
        CancellationToken cancellationToken = default)
    {
        var result = await _sender.Send(new YieldSquadAssociatesQuery
        {
            EntityLinqFacade = GenerateWhereClauseFacadeFrom(queryOptions)
        }, cancellationToken);
        
        return result.Match(
            Ok,
            Problem
        );
    }
    
    [Route("{squadId:int}-{associateId:int}")]
    [HttpGet]
    [ProducesResponseType(typeof(SquadModel), (int)HttpStatusCode.OK)]
    [DistribuTeEnableQuery()]
    public async Task<IActionResult> GetByIdAsync(int squadId, int associateId, ODataQueryOptions<SquadAssociateModel> queryOptions,
        CancellationToken cancellationToken = default)
    {
        var result = await _sender.Send(new PickSquadAssociateQuery
        {
            EntityLinqFacade = GenerateWhereClauseFacadeFrom(queryOptions),
            SquadId = squadId,
            AssociateId = associateId
        }, cancellationToken);
        
        return result.Match(
            Ok,
            Problem
        );
    }
}