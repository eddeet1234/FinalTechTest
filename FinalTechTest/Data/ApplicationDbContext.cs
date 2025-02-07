namespace FinalTechTest.Data
{
    using Microsoft.EntityFrameworkCore;
    using FinalTechTest.Models;
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<WeatherForecast> WeatherForecasts { get; set; }
        public DbSet<DailyForecast> DailyForecasts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure one-to-many relationship
            modelBuilder.Entity<WeatherForecast>()
                .HasMany(w => w.DailyForecasts)
                .WithOne(d => d.WeatherForecast)
                .HasForeignKey(d => d.WeatherForecastId);
        }
    }
}

