using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DockerDemoAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly DatabaseContext context;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, DatabaseContext context)
        {
            _logger = logger;
            this.context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {
            return await context.WeatherForecasts.ToListAsync();
        }
        [HttpGet("/addWeather")]
        public async Task AddWeatherAsync()
        {
            var rng = new Random();
            var newWeather = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            });
            await context.WeatherForecasts.AddRangeAsync(newWeather);
            await context.SaveChangesAsync();
        }
    }
}
