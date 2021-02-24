using Microsoft.Extensions.Diagnostics.HealthChecks;
using MyHealthCheckWebApp.Clients;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MyHealthCheckWebApp.HealthChecks
{
  public class MyCustomCheck : IHealthCheck
  {
    private readonly IWeatherForecastClient _weatherForecastClient;

    public MyCustomCheck(IWeatherForecastClient weatherForecastClient)
    {
      _weatherForecastClient = weatherForecastClient;
    }

    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
      HealthCheckResult healthCheckResult;
      try
      {
        var myresult = await _weatherForecastClient.GetWeatherForecasts();
        var weatherforecasts = myresult.ToList();
        healthCheckResult = HealthCheckResult.Healthy($"Retrieved {myresult.Count()} entries.");
      }
      catch(Exception ex)
      {
        healthCheckResult = HealthCheckResult.Unhealthy(ex.Message);
      }

      return healthCheckResult;
    }
  }

  //public interface IMyCustomService
  //{

  //  public bool IsHealthy();

  //}

  //public class MyCustomService : IMyCustomService
  //{

  //  public bool IsHealthy()
  //  {
  //    return new Random().NextDouble() > 0.5;
  //  }

  //}
}
