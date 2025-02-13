using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FinalTechTest.Models
{
    public class CurrentWeather
    {
        [Key]
        public int Id { get; set; }

        public long Timestamp { get; set; } // Unix timestamp for current weather
        public long Sunrise { get; set; } // Time of sunrise in Unix timestamp
        public long Sunset { get; set; } // Time of sunset in Unix timestamp
        public double Temperature { get; set; } // Current temperature
        public double FeelsLike { get; set; } // Perceived temperature
        public int Pressure { get; set; } // Atmospheric pressure at sea level
        public int Humidity { get; set; } // Atmospheric humidity
        public double DewPoint { get; set; } // Temperature at which dew forms
        public double UvIndex { get; set; } // Strength of ultraviolet radiation
        public int CloudCover { get; set; } // Percentage of cloudiness
        public int Visibility { get; set; } // Maximum visibility in meters
        public double WindSpeed { get; set; } // Speed of wind
        public double WindDeg { get; set; } // Wind direction in degrees
        public double? WindGust { get; set; } // Sudden burst of wind
        public string Weather { get; set; } = string.Empty; // Current weather condition

        [ForeignKey("WeatherForecast")]
        public int WeatherForecastId { get; set; }
        public WeatherForecast WeatherForecast { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // New field
    }
}


