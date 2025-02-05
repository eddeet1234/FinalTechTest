namespace FinalTechTest.Services
{
    using System.Net.Http;
    using System.Net.Http.Json;
    using System.Threading.Tasks;
    using FinalTechTest.Data;
    using FinalTechTest.Models;
    using Microsoft.EntityFrameworkCore;

    public class WeatherService
    {
        private readonly HttpClient _httpClient;
        private readonly ApplicationDbContext _dbContext;
        private readonly string _apiKey = "10288c9849305809b834891e197cebb5"; // Replace with your key

        public WeatherService(HttpClient httpClient,ApplicationDbContext dbContext)
        {
            _httpClient = httpClient;
            _dbContext = dbContext;
        }

        public async Task<WeatherForecastDto?> GetWeatherAsync(double lat, double lon)
        {
            string url = $"https://api.openweathermap.org/data/3.0/onecall?lat={lat}&lon={lon}&exclude=minutely,hourly,alerts&units=metric&appid={_apiKey}";

            var weatherData = await _httpClient.GetFromJsonAsync<WeatherForecastDto>(url);

            if (weatherData != null)
            {
                Console.WriteLine("Weather Forecast:");
                foreach (var day in weatherData.Daily)
                {
                    string date = DateTimeOffset.FromUnixTimeSeconds(day.DateTimestamp).ToString("yyyy-MM-dd");
                    Console.WriteLine($" {date} |  Min: {day.Temp.Min}°C - Max: {day.Temp.Max}°C |  Wind: {day.WindSpeed} km/h | {day.Weather[0].Description}");
                }


                var forecastEntity = new WeatherForecast
                {
                    Latitude = lat,
                    Longitude = lon,
                    DailyForecasts = weatherData.Daily.Select(day => new DailyForecast
                    {
                        DateTimestamp = day.DateTimestamp,
                        MinTemp = day.Temp.Min,
                        MaxTemp = day.Temp.Max,
                        Humidity = day.Humidity,
                        WindSpeed = day.WindSpeed,
                        UvIndex = day.UvIndex,
                        Rain = day.Rain ?? 0,
                        Description = day.Weather[0].Description
                    }).ToList()
                };

                _dbContext.WeatherForecasts.Add(forecastEntity);
                await _dbContext.SaveChangesAsync(); // Save to database

                return weatherData;


                
            }
            else
            {

                Console.WriteLine("Failed to fetch weather data.");
                return null;
            }
        }

        public async Task<List<LocationDto>> GetAllLocationsAsync()
        {
            var locations = await _dbContext.WeatherForecasts
                .Select(f => new LocationDto
                {
                    Latitude = f.Latitude,
                    Longitude = f.Longitude
                })
                .Distinct()
                .ToListAsync();

            return locations;
        }
    }

}
