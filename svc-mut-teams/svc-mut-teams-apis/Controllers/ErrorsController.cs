namespace DistribuTe.Mutators.Teams.Apis.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

[ApiController]
public class ErrorsController : ControllerBase
{
    [Route("error-development")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public IActionResult HandleErrorDevelopment([FromServices] IHostEnvironment hostEnvironment)
    {
        if (!hostEnvironment.IsDevelopment())
            return NotFound();

        var exceptionHandlerFeature = HttpContext.Features.Get<IExceptionHandlerFeature>()!;

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