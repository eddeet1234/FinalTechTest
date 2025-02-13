namespace FinalTechTest.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class WeatherForecast
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public ICollection<DailyForecast> DailyForecasts { get; set; } // One-to-many relationship
        public CurrentWeather CurrentWeather { get; set; }
    }
}
