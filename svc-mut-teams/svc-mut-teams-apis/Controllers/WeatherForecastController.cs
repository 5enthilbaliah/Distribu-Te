namespace DistribuTe.Mutators.Teams.Apis.Controllers;

using Application.WeatherForecast.Dtos;
using Application.WeatherForecast.Queries;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("protected/[controller]")]
[ApiVersion("1.0")]
[Produces("application/json")]
public class WeatherForecastController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    
    
    [HttpGet]
    [Route("")]
    [ProducesResponseType(typeof(IEnumerable<WeatherForecastDto>), 200)]
    public async Task<IActionResult> Get(CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(new GetRandomWeatherForecastQuery(),
            cancellationToken).ConfigureAwait(false);
        
        return Ok(result);
    }
}