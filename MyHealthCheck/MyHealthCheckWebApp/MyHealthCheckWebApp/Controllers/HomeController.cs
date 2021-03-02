using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyHealthCheckWebApp.Clients;
using MyHealthCheckWebApp.Models;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MyHealthCheckWebApp.Controllers
{
  public class HomeController : Controller
  {
    private readonly ILogger<HomeController> _logger;
    private readonly IWeatherForecastClient _weatherForecastClient;


    public HomeController(ILogger<HomeController> logger,
     IWeatherForecastClient weatherForecastClient)
    {
      _logger = logger;
      _weatherForecastClient = weatherForecastClient;
    }

    public IActionResult Index()
    {
      return View();
    }

    public async Task<IActionResult> WeatherForecast()
    {
      var result = await _weatherForecastClient.GetWeatherForecasts();
      ViewBag.WeatherForecasts = result.ToList();
      return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
      return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
  }
}
