using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using MyHealthCheckWebApi.Models;

namespace MyHealthCheckWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly WeatherForecastDbContext _weatherForecastDbContext;

        public WeatherForecastController(ILogger<WeatherForecastController> logger,
          WeatherForecastDbContext weatherForecastDbContext)
        {
            _logger = logger;
            _weatherForecastDbContext = weatherForecastDbContext;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
           return _weatherForecastDbContext.WeatherForecasts.ToList();
        }
    }
}
