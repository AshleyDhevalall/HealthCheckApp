using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MyHealthCheckWebApp.HealthChecks
{
  public class MyCustomCheck : IHealthCheck
  {
    private readonly IMyCustomService _customService;

    public MyCustomCheck(IMyCustomService customService)
    {
      _customService = customService;
    }

    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
      var result = _customService.IsHealthy() ? HealthCheckResult.Healthy() : HealthCheckResult.Unhealthy();
      return Task.FromResult(result);
    }
  }

  public interface IMyCustomService
  {

    public bool IsHealthy();

  }

  public class MyCustomService : IMyCustomService
  {

    public bool IsHealthy()
    {
      return new Random().NextDouble() > 0.5;
    }

  }
}
