using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DockerDemo.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IConfiguration config;
        public IEnumerable<WeatherForecast> WeatherData;

        public IndexModel(ILogger<IndexModel> logger, IConfiguration config)
        {
            _logger = logger;
            this.config = config;
        }

        public async Task OnGet()
        {
            var weatherService = RestService.For<IWeatherService>(config["API_ADDRESS"]);
            WeatherData = await weatherService.GetWeatherAsync();
        }
    }

    public interface IWeatherService
    {
        [Get("/weatherForecast")]
        Task<IEnumerable<WeatherForecast>> GetWeatherAsync();
    }


    public class WeatherForecast
    {
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string Summary { get; set; }
    }
}
