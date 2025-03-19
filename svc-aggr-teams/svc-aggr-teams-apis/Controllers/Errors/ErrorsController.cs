namespace DistribuTe.Aggregates.Teams.Apis.Controllers.Errors;

using Framework.AppEssentials;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.OData;

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
        var statusCode = exceptionHandlerFeature.Error is ODataException ? 400 : 500;
        return Problem(
            detail: exceptionHandlerFeature.Error.StackTrace,
            title: exceptionHandlerFeature.Error.Message,
            instance: exceptionHandlerFeature.Path,
            statusCode: statusCode);
    }

    [Route("error")]
    [ApiExplorerSettings(IgnoreApi = true)]
    [AllowAnonymous]
    public IActionResult HandleError([FromServices] IRequestContext requestContext)
    {
        var exceptionHandlerFeature = requestContext.GetFeature<IExceptionHandlerFeature>()!;
        return exceptionHandlerFeature.Error is ODataException ? Problem(
            title: exceptionHandlerFeature.Error.Message,
            instance: exceptionHandlerFeature.Path,
            statusCode: 400) : Problem(title: "Ah snap!!! this is embarrassing, please contact the administrator - NOW!!!",
            instance: exceptionHandlerFeature.Path,
            statusCode: 500);
    }
}