namespace FinalTechTest.Models
{
    using System.Text.Json.Serialization;
    using System.Collections.Generic;

    public class WeatherForecastDto
    {
        [JsonPropertyName("current")]
        public CurrentWeatherDto Current { get; set; } = new();

        [JsonPropertyName("daily")]
        public List<DailyForecastDto> Daily { get; set; } = new();  // Stores multiple days
    }

    public class CurrentWeatherDto
    {
        [JsonPropertyName("dt")]
        public long Timestamp { get; set; }  // Unix timestamp for current weather

        [JsonPropertyName("sunrise")]
        public long Sunrise { get; set; }  // Time of sunrise in Unix timestamp

        [JsonPropertyName("sunset")]
        public long Sunset { get; set; }  // Time of sunset in Unix timestamp

        [JsonPropertyName("temp")]
        public double Temperature { get; set; }  // Current temperature

        [JsonPropertyName("feels_like")]
        public double FeelsLike { get; set; }  // Perceived temperature

        [JsonPropertyName("pressure")]
        public int Pressure { get; set; }  // Atmospheric pressure at sea level

        [JsonPropertyName("humidity")]
        public int Humidity { get; set; }  // Atmospheric humidity

        [JsonPropertyName("dew_point")]
        public double DewPoint { get; set; }  // Temperature at which dew forms

        [JsonPropertyName("uvi")]
        public double UvIndex { get; set; }  // Strength of ultraviolet radiation

        [JsonPropertyName("clouds")]
        public int CloudCover { get; set; }  // Percentage of cloudiness

        [JsonPropertyName("visibility")]
        public int Visibility { get; set; }  // Maximum visibility in meters

        [JsonPropertyName("wind_speed")]
        public double WindSpeed { get; set; }  // Speed of wind

        [JsonPropertyName("wind_deg")]
        public double WindDeg { get; set; }  // Wind direction in degrees

        [JsonPropertyName("wind_gust")]
        public double? WindGust { get; set; }  // Sudden burst of wind

        [JsonPropertyName("weather")]
        public List<WeatherDescription> Weather { get; set; } = new();  // Current weather condition
    }

    public class DailyForecastDto
    {
        [JsonPropertyName("dt")]
        public long DateTimestamp { get; set; }  // Unix timestamp for the day

        [JsonPropertyName("temp")]
        public Temperature Temp { get; set; } = new();

        [JsonPropertyName("humidity")]
        public int Humidity { get; set; }

        [JsonPropertyName("wind_speed")]
        public double WindSpeed { get; set; }

        [JsonPropertyName("wind_deg")]
        public double WindDeg { get; set; }

        [JsonPropertyName("uvi")]
        public double UvIndex { get; set; }

        [JsonPropertyName("rain")]
        public double? Rain { get; set; }  // Nullable to handle missing rain data

        [JsonPropertyName("weather")]
        public List<WeatherDescription> Weather { get; set; } = new();
    }

    public class Temperature
    {
        [JsonPropertyName("min")]
        public double Min { get; set; }

        [JsonPropertyName("max")]
        public double Max { get; set; }
    }

    public class WeatherDescription
    {
        [JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty;  // Weather summary
    }

}
