namespace DistribuTe.Aggregates.Projects.Apis.Controllers;

using System.Net;
using Application.ProjectCategories;
using Application.ProjectCategories.DataContracts;
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

[Route("protected/project-categories")]
[ApiVersion("1.0")]
[Produces("application/json")]
[Authorize(Roles = "read-projects,read-projects-categories")]
public class ProjectCategoryQueryController(ISender sender, OdataFilterVisitor visitor,
    IOdataNavigator<ProjectCategoryModel> navigator, IRequestContext requestContext) : 
    DistribuTeQueryController<ProjectCategoryModel>(visitor, navigator, requestContext)
{
    private readonly ISender _sender = sender ?? throw new ArgumentNullException(nameof(sender));

    [HttpGet]
    [Route("")]
    [ProducesResponseType(typeof(IList<ProjectCategoryModel>), (int)HttpStatusCode.OK)]
    [DistribuTeEnableQuery()]
    public async Task<IActionResult> SearchAsync(ODataQueryOptions<ProjectCategoryModel> queryOptions,
        CancellationToken cancellationToken = default)
    {
        var result = await _sender.Send(new YieldProjectCategoriesQuery
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
    [ProducesResponseType(typeof(ProjectCategoryModel), (int)HttpStatusCode.OK)]
    [DistribuTeEnableQuery()]
    public async Task<IActionResult> GetByIdAsync(int id, ODataQueryOptions<ProjectCategoryModel> queryOptions,
        CancellationToken cancellationToken = default)
    {
        var result = await _sender.Send(new PickProjectCategoryQuery
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