namespace DistribuTe.Aggregates.Teams.Apis.Controllers;

using System.Net;
using Application.SquadAssociates;
using Application.SquadAssociates.DataContracts;
using Application.Squads.DataContracts;
using Framework.ApiEssentials.Odata;
using Framework.ApiEssentials.Odata.Controllers;
using Framework.ApiEssentials.Odata.Implementations;
using Framework.AppEssentials.Implementations;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

public class SquadAssociateAggregateController(ISender sender, OdataFilterVisitor<WhereClauseItem> visitor,
    IOdataNavigator<SquadAssociateModel, WhereClauseItem> navigator) : 
    DistribuTeAggregateController<SquadAssociateModel>(visitor, navigator)
{
    private readonly ISender _sender = sender ?? throw new ArgumentNullException(nameof(sender));
    
    [HttpGet]
    [Route("")]
    [ProducesResponseType(typeof(IList<SquadAssociateModel>), (int)HttpStatusCode.OK)]
    [EnableQuery()]
    public async Task<IActionResult> SearchAsync(ODataQueryOptions<SquadAssociateModel> queryOptions,
        CancellationToken cancellationToken = default)
    {
        var result = await _sender.Send(new YieldSquadAssociatesQuery
        {
            LinqQueryFacade = (GenerateWhereClauseFacadeFrom(queryOptions) as LinqQueryFacade)!
        }, cancellationToken);
        
        return result.Match(
            Ok,
            Problem
        );
    }
    
    [Route("{squadId:int}-{associateId:int}")]
    [HttpGet]
    [ProducesResponseType(typeof(SquadModel), (int)HttpStatusCode.OK)]
    [EnableQuery()]
    public async Task<IActionResult> GetByIdAsync(int squadId, int associateId, ODataQueryOptions<SquadAssociateModel> queryOptions,
        CancellationToken cancellationToken = default)
    {
        var result = await _sender.Send(new PickSquadAssociateQuery
        {
            LinqQueryFacade = (GenerateWhereClauseFacadeFrom(queryOptions) as LinqQueryFacade)!,
            SquadId = squadId,
            AssociateId = associateId
        }, cancellationToken);
        
        return result.Match(
            Ok,
            Problem
        );
    }
}