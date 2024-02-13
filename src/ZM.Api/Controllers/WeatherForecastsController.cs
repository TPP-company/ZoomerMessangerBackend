using Template.Application.UseCases.WeatherForecasts.GetWeatherForecasts;
using Microsoft.AspNetCore.Mvc;
using ZM.Domain.Entities;

namespace ZM.Api.Controllers;

[ApiController]
[Route("weather-forecasts")]
public class WeatherForecastsController : ApiControllerBase
{
    [HttpGet]
    public Task<WeatherForecast[]> Get()
        => Sender.Send(new GetWeatherForecastsQuery());
}
