using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZM.Application.Dependencies.Infrastructure.Authentication;
using ZM.Application.UseCases.WeatherForecasts.GetWeatherForecasts;
using ZM.Domain.Entities;

namespace ZM.Api.Controllers;

[ApiController]
[Route("weather-forecasts")]
public class WeatherForecastsController(ICurrentUser _currentUser) : ApiControllerBase
{
	[HttpGet]
	public Task<WeatherForecast[]> Get()
		=> Sender.Send(new GetWeatherForecastsQuery());

	[Authorize]
	[HttpGet("authorized")]
	public async Task<WeatherForecastAuthorized> GetAuthorized()
	{
		return new WeatherForecastAuthorized(await Sender.Send(new GetWeatherForecastsQuery()), _currentUser.ExternalId);
	}
}

public record WeatherForecastAuthorized(WeatherForecast[] Weathers, Guid CurrentUserId);
