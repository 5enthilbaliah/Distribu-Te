// ReSharper disable once CheckNamespace
namespace DistribuTe.Mutators.Teams.Application.WeatherForecast.Dtos;

public class WeatherForecastDto
{
    public DateTime Date { get; set; }

    public int TemperatureC { get; set; }

    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

    public string? Summary { get; set; }
}