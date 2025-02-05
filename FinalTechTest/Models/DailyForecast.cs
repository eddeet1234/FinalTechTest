using System.ComponentModel.DataAnnotations;

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
        public double UvIndex { get; set; }
        public double? Rain { get; set; }
        public string Description { get; set; } = string.Empty;

        public int WeatherForecastId { get; set; }
        public WeatherForecast WeatherForecast { get; set; } // Foreign Key

    }
}
