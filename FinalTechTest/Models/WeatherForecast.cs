namespace FinalTechTest.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class WeatherForecast
    {
        [Key]
        public int Id { get; set; }

        public double Latitude { get; set; }
        public double Longitude { get; set; }

        [InverseProperty("WeatherForecast")]
        public List<DailyForecast> DailyForecasts { get; set; } = new();
    }
}
