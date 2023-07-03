using Microsoft.AspNetCore.Mvc;
using FirstNewman.Api.Models;

namespace FirstNewman.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries =
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get([FromQuery] int days)
    {
        _logger.LogInformation("Received {Action} request", nameof(Get));
        IEnumerable<WeatherForecast> forecast = Enumerable.Range(1, days)
            .Select(index => new WeatherForecast
                    {
                        Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                        TemperatureC = Random.Shared.Next(-20, 55),
                        Summary = Summaries[Random.Shared.Next(Summaries.Length)]
                    });
        _logger.LogInformation("Responding with {@Forecast}", forecast);
        return forecast;
    }
}

