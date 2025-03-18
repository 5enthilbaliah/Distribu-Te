namespace DistribuTe.Aggregates.Teams.Apis.Controllers.Errors;

using Framework.AppEssentials;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

[ApiController]
public class ErrorsController : ControllerBase
{
    [Route("error-development")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public IActionResult HandleErrorDevelopment([FromServices] IHostEnvironment hostEnvironment,
        [FromServices] IRequestContext requestContext)
    {
        if (!hostEnvironment.IsDevelopment())
            return NotFound();

        var exceptionHandlerFeature = requestContext.GetFeature<IExceptionHandlerFeature>()!;
        return Problem(
            detail: exceptionHandlerFeature.Error.StackTrace,
            title: exceptionHandlerFeature.Error.Message,
            instance: exceptionHandlerFeature.Path);
    }

    [Route("error")]
    [ApiExplorerSettings(IgnoreApi = true)]
    [AllowAnonymous]
    public IActionResult HandleError() => Problem();
}