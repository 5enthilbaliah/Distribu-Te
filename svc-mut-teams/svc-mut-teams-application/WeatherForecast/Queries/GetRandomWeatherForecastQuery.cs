namespace DistribuTe.Mutators.Teams.Application.WeatherForecast.Queries;

using Dtos;
using MediatR;

public class GetRandomWeatherForecastQuery : IRequest<IEnumerable<WeatherForecastDto>>
{ }