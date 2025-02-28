namespace DistribuTe.Mutators.Teams.Application.WeatherForecast;

using Dtos;
using MediatR;
using Queries;

public class WeatherForecastQueryHandler : IRequestHandler<GetRandomWeatherForecastQuery, IEnumerable<WeatherForecastDto>>
{
    private static readonly string[] Summaries =
    [
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    ];
    
    public async Task<IEnumerable<WeatherForecastDto>> Handle(GetRandomWeatherForecastQuery request, CancellationToken cancellationToken)
    {
        var rng = new Random();
        return await Task.FromResult(Enumerable.Range(1, 5).Select(index => new WeatherForecastDto
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            }).ToArray());
    }
}