using MyHealthCheckWebApp.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace MyHealthCheckWebApp.Clients
{
  public class WeatherForecastClient : IWeatherForecastClient
  {
    private readonly HttpClient httpClient;
    private const string url = "http://localhost/myhealthcheckwebapi/weatherforecast";

    public WeatherForecastClient(HttpClient httpClient)
    {
      this.httpClient = httpClient;
    }

    public async Task<IEnumerable<WeatherForecast>> GetWeatherForecasts()
    {
      var response = await httpClient.GetAsync(url);
      var body = await response.Content.ReadAsStringAsync();
      var posts = JsonSerializer.Deserialize<IEnumerable<WeatherForecast>>(body);
      return posts;
    }
  }
}
