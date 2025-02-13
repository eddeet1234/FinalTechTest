namespace FinalTechTest.Data
{
    using Microsoft.EntityFrameworkCore;
    using FinalTechTest.Models;
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<WeatherForecast> WeatherForecasts { get; set; }
        public DbSet<DailyForecast> DailyForecasts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure one-to-many relationship between WeatherForecast and DailyForecast
            modelBuilder.Entity<WeatherForecast>()
                .HasMany(w => w.DailyForecasts)
                .WithOne(d => d.WeatherForecast)
                .HasForeignKey(d => d.WeatherForecastId);

            // Configure one-to-one relationship between WeatherForecast and CurrentWeather
            modelBuilder.Entity<WeatherForecast>()
                .HasOne(w => w.CurrentWeather)
                .WithOne(c => c.WeatherForecast)
                .HasForeignKey<CurrentWeather>(c => c.WeatherForecastId);
        }
    }
}

