namespace FinalTechTest.Services
{
    using System.Text.Json.Serialization;
    using System.Collections.Generic;

    public class WeatherForecastDto
    {
        [JsonPropertyName("daily")]
        public List<DailyForecastDto> Daily { get; set; } = new();  // Stores multiple days
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
