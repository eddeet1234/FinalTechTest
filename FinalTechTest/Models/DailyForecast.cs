using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalTechTest.Models
{
    public class DailyForecast
    {
        [Key]
        public int Id { get; set; }

        public long DateTimestamp { get; set; }
        public double MinTemp { get; set; }
        public double MaxTemp { get; set; }
        public int Humidity { get; set; }
        public double WindSpeed { get; set; }
        public double WindDeg { get; set; }
        public double UvIndex { get; set; }
        public double? Rain { get; set; }
        public string Description { get; set; } = string.Empty;

        [ForeignKey("WeatherForecast")]
        public int WeatherForecastId { get; set; }
        public WeatherForecast WeatherForecast { get; set; } //Navigation Property (You lose the ability to use eager loading (e.g., .Include()) to load related WeatherForecast data as part of your DailyForecast queries. Eager loading relies on navigation properties to include related data in a single query.)

         public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // New field
         
    }
}
