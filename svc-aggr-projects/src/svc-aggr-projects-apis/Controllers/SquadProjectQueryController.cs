namespace DistribuTe.Aggregates.Projects.Apis.Controllers;

using System.Net;
using Application.SquadProjects;
using Application.SquadProjects.DataContracts;
using Asp.Versioning;
using Framework.ApiEssentials.Odata;
using Framework.ApiEssentials.Odata.Controllers;
using Framework.ApiEssentials.Odata.Filters;
using Framework.ApiEssentials.Odata.Implementations;
using Framework.AppEssentials;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

[Route("protected/squad-projects")]
[ApiVersion("1.0")]
[Produces("application/json")]
[Authorize(Roles = "read-projects")]
public class SquadProjectQueryController(ISender sender, OdataFilterVisitor visitor,
    IOdataNavigator<SquadProjectModel> navigator, IRequestContext requestContext) : 
    DistribuTeQueryController<SquadProjectModel>(visitor, navigator, requestContext)
{
    private readonly ISender _sender = sender ?? throw new ArgumentNullException(nameof(sender));
    
    [HttpGet]
    [Route("")]
    [ProducesResponseType(typeof(IList<SquadProjectModel>), (int)HttpStatusCode.OK)]
    [DistribuTeEnableQuery()]
    public async Task<IActionResult> SearchAsync(ODataQueryOptions<SquadProjectModel> queryOptions,
        CancellationToken cancellationToken = default)
    {
        var result = await _sender.Send(new YieldSquadProjectsQuery
        {
            EntityLinqFacade = GenerateWhereClauseFacadeFrom(queryOptions)
        }, cancellationToken);
        
        return result.Match(
            Ok,
            Problem
        );
    }
    
    [Route("{squadId:int}-{projectId:int}")]
    [HttpGet]
    [ProducesResponseType(typeof(SquadProjectModel), (int)HttpStatusCode.OK)]
    [DistribuTeEnableQuery()]
    public async Task<IActionResult> GetByIdAsync(int squadId, int projectId, ODataQueryOptions<SquadProjectModel> queryOptions,
        CancellationToken cancellationToken = default)
    {
        var result = await _sender.Send(new PickSquadProjectQuery
        {
            EntityLinqFacade = GenerateWhereClauseFacadeFrom(queryOptions),
            SquadId = squadId,
            ProjectId = projectId
        }, cancellationToken);
        
        return result.Match(
            Ok,
            Problem
        );
    }
}