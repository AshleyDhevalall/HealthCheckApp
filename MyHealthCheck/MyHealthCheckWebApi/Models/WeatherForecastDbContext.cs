using Microsoft.EntityFrameworkCore;

namespace MyHealthCheckWebApi.Models
{
  public class WeatherForecastDbContext : DbContext
  {
    public WeatherForecastDbContext(DbContextOptions<WeatherForecastDbContext> options)
        : base(options) { }
    public DbSet<WeatherForecast> WeatherForecasts { get; set; }
  }
}
