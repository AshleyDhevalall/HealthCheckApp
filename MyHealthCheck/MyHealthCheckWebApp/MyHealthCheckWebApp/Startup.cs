using MyHealthCheckWebApp.HealthChecks;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using MyHealthCheckWebApp.Clients;
using System;

namespace MyHealthCheckWebApp
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddHealthChecks()
    //  .AddCheck<MyCustomCheck>("MyCustomCheck", HealthStatus.Unhealthy, new string[] { "tag1" })
      .AddUrlGroup(new Uri("http://localhost/myhealthcheckwebapi/weatherforecast"), "MyHealthCheckWebApi", HealthStatus.Unhealthy)
      .AddDiskStorageHealthCheck(s => s.AddDrive("C:\\", 1024)) // 1024 MB (1 GB) free minimum
      .AddProcessAllocatedMemoryHealthCheck(512); // 512 MB max allocated memory;

      services.AddHttpClient<IWeatherForecastClient, WeatherForecastClient>();
  //    services.AddScoped<IMyCustomService, MyCustomService>();
      services.AddHealthChecksUI();
      services.AddControllersWithViews();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      app.UseHealthChecks("/health", new HealthCheckOptions()
      {
        Predicate = _ => true,
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
        ResultStatusCodes =
        {
            [HealthStatus.Healthy] = StatusCodes.Status200OK,
            [HealthStatus.Degraded] = StatusCodes.Status200OK,
            [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable
        }
      });

      app.UseStaticFiles();
      app.UseHealthChecksUI();

      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }


      app.UseRouting();

      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllerRoute(
                  name: "default",
                  pattern: "{controller=Home}/{action=Index}/{id?}");
      });
    }
  }
}
