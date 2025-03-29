namespace DistribuTe.Aggregates.Projects.Apis.Controllers;

using System.Net;
using Application.Projects;
using Application.Projects.DataContracts;
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

[Route("protected/projects")]
[ApiVersion("1.0")]
[Produces("application/json")]
[Authorize(Roles = "read-projects,read-projects-projects")]
public class ProjectQueryController(ISender sender, OdataFilterVisitor visitor,
    IOdataNavigator<ProjectModel> navigator, IRequestContext requestContext) : 
    DistribuTeQueryController<ProjectModel>(visitor, navigator, requestContext)
{
    private readonly ISender _sender = sender ?? throw new ArgumentNullException(nameof(sender));

    [HttpGet]
    [Route("")]
    [ProducesResponseType(typeof(IList<ProjectModel>), (int)HttpStatusCode.OK)]
    [DistribuTeEnableQuery()]
    public async Task<IActionResult> SearchAsync(ODataQueryOptions<ProjectModel> queryOptions,
        CancellationToken cancellationToken = default)
    {
        var result = await _sender.Send(new YieldProjectsQuery
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
    [ProducesResponseType(typeof(ProjectModel), (int)HttpStatusCode.OK)]
    [DistribuTeEnableQuery()]
    public async Task<IActionResult> GetByIdAsync(int id, ODataQueryOptions<ProjectModel> queryOptions,
        CancellationToken cancellationToken = default)
    {
        var result = await _sender.Send(new PickProjectQuery
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