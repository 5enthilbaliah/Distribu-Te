namespace DistribuTe.Aggregates.Teams.Apis.Controllers;

using System.Net;
using Application.Associates;
using Application.Associates.DataContracts;
using Asp.Versioning;
using Framework.ApiEssentials.Odata;
using Framework.ApiEssentials.Odata.Controllers;
using Framework.ApiEssentials.Odata.Implementations;
using Framework.AppEssentials.Implementations;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

[Route("protected/aggregates/associates")]
[ApiVersion("1.0")]
[Produces("application/json")]
public class AssociateAggregateController(ISender sender, OdataFilterVisitor<WhereClauseItem> visitor,
    IOdataNavigator<AssociateModel, WhereClauseItem> navigator) : 
    DistribuTeAggregateController<AssociateModel>(visitor, navigator)
{
    private readonly ISender _sender = sender ?? throw new ArgumentNullException(nameof(sender));

    [HttpGet]
    [Route("")]
    [ProducesResponseType(typeof(IList<AssociateModel>), (int)HttpStatusCode.OK)]
    [EnableQuery()]
    public async Task<IActionResult> SearchAsync(ODataQueryOptions<AssociateModel> queryOptions,
        CancellationToken cancellationToken = default)
    {
        var result = await _sender.Send(new YieldAssociatesQuery
        {
            WhereClauseFacade = (GenerateWhereClauseFacadeFrom(queryOptions) as WhereClauseFacade)!
        }, cancellationToken);
        
        return result.Match(
            Ok,
            Problem
        );
    }

    [Route("{id:int}")]
    [HttpGet]
    [ProducesResponseType(typeof(AssociateModel), (int)HttpStatusCode.OK)]
    [EnableQuery()]
    public async Task<IActionResult> GetByIdAsync(int id, ODataQueryOptions<AssociateModel> queryOptions,
        CancellationToken cancellationToken = default)
    {
        var result = await _sender.Send(new PickAssociateQuery
        {
            WhereClauseFacade = (GenerateWhereClauseFacadeFrom(queryOptions) as WhereClauseFacade)!,
            Id = id
        }, cancellationToken);
        
        return result.Match(
            Ok,
            Problem
        );
    }
}