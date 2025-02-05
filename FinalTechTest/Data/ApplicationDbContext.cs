namespace FinalTechTest.Data
{
    using Microsoft.EntityFrameworkCore;
    using FinalTechTest.Models;
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<WeatherForecast> WeatherForecasts { get; set; }
        public DbSet<DailyForecast> DailyForecasts { get; set; }
    }
}

