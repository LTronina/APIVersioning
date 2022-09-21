using Microsoft.AspNetCore.Mvc;

namespace APIVersioning.Controllers
{
    [ApiController]
    [Route("WeatherForecast")]
    [ApiVersion("2.0")]
    [ApiVersion("1.0", Deprecated = true)]
    public class WeatherForecastV1Controller : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastV1Controller> _logger;

        public WeatherForecastV1Controller(ILogger<WeatherForecastV1Controller> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        [MapToApiVersion("2.0")]
        public IEnumerable<WeatherForecast> GetV2()
        {
            return Enumerable.Range(1, 2).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet(Name = "GetWeatherForecast")]
        [MapToApiVersion("1.0")]
        public IEnumerable<WeatherForecast> GetV1()
        {
            return Enumerable.Range(1, 1).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

    }
}