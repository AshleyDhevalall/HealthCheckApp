using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyHealthCheckWebApi.Models;

namespace MyHealthCheckWebApi
{
  public class Startup
  {
    public string LocalIPAddr { get; private set; }

    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
      services.AddControllers();

      services.AddDbContext<WeatherForecastDbContext>(o => o.UseSqlServer(Configuration["ConnectionString"]));

      services.AddHealthChecks()
          .AddDiskStorageHealthCheck(s => s.AddDrive("C:\\", 1024))
          .AddProcessAllocatedMemoryHealthCheck(512)
          .AddSqlServer(Configuration["ConnectionString"]);

      services
          .AddHealthChecksUI(s =>
          {
            s.AddHealthCheckEndpoint("HealthChecksExample", "http://localhost/myhealthcheckwebapi/health");
          })
      .AddSqliteStorage("Data Source = healthchecks.db");
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseHttpsRedirection();

      app.UseRouting();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
        endpoints.MapHealthChecksUI();

        endpoints.MapHealthChecks("/health", new HealthCheckOptions()
        {
          Predicate = _ => true,
          ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        });
      });
    }
  }
}
