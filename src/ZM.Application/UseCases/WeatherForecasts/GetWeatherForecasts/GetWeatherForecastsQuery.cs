using MediatR;
using ZM.Domain.Entities;

namespace ZM.Application.UseCases.WeatherForecasts.GetWeatherForecasts;

public record GetWeatherForecastsQuery() : IRequest<WeatherForecast[]>;

public class GetWeatherForecastsQueryHandler : IRequestHandler<GetWeatherForecastsQuery, WeatherForecast[]>
{
	private static readonly string[] Summaries =
	[
		"Freezing",
		"Bracing",
		"Chilly",
		"Cool",
		"Mild",
		"Warm",
		"Balmy",
		"Hot",
		"Sweltering",
		"Scorching"
	];

	public async Task<WeatherForecast[]> Handle(GetWeatherForecastsQuery request, CancellationToken cancellationToken)
	{
		return Enumerable.Range(1, 5).Select(index => new WeatherForecast
		{
			Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
			TemperatureC = Random.Shared.Next(-20, 55),
			Summary = Summaries[Random.Shared.Next(Summaries.Length)]
		})
	   .ToArray();
	}
}
