using MyHealthCheckWebApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyHealthCheckWebApp.Clients
{
  public interface IWeatherForecastClient
  {
    Task<IEnumerable<WeatherForecast>> GetWeatherForecasts();
  }
}
