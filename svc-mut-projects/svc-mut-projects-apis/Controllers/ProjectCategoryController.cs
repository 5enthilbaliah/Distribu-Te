namespace DistribuTe.Mutators.Projects.Apis.Controllers;

using System.Net;
using Application.ProjectCategories;
using Application.ProjectCategories.DataContracts;
using Asp.Versioning;
using Framework.ApiEssentials.Odata.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

[Route("protected/project-categories")]
[ApiVersion("1.0")]
[Produces("application/json")]
public class ProjectCategoryController(IMediator mediator) : DistribuTeController
{
    private readonly IMediator _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    
    [Route("")]
    [HttpPost]
    [ProducesResponseType(typeof(ProjectCategoryResponse), (int)HttpStatusCode.Created)]
    [EnableQuery()]
    public async Task<IActionResult> SpawnAsync([FromBody] ProjectCategoryRequest projectCategory, CancellationToken ct = default)
    {
        var result = await _mediator.Send(new SpawnProjectCategoryCommand
        {
            ProjectCategory = projectCategory,
        }, ct).ConfigureAwait(false);

        return result.Match(
            validResult => StatusCode((int)HttpStatusCode.Created, validResult),
            Problem
        );
    }

    [Route("{id:int}")]
    [HttpPut]
    [ProducesResponseType(typeof(ProjectCategoryResponse), (int)HttpStatusCode.OK)]
    [EnableQuery()]
    public async Task<IActionResult> CommitAsync(int id, [FromBody] ProjectCategoryRequest projectCategory, CancellationToken ct = default)
    {
        var result = await _mediator.Send(new CommitProjectCategoryCommand
        {
            ProjectCategory = projectCategory,
            Id = id,
        }, ct).ConfigureAwait(false);
        
        return result.Match(
            Ok,
            Problem
        );
    }
    
    [Route("{id:int}")]
    [HttpDelete]
    [ProducesResponseType(typeof(ProjectCategoryResponse), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> TrashAsync(int id, CancellationToken ct = default)
    {
        var result = await _mediator.Send(new TrashProjectCategoryCommand
        {
            Id = id,
        }, ct).ConfigureAwait(false);
        
        return result.Match(
            _ => NoContent(),
            Problem
        );
    }
}