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

        public WeatherService(HttpClient httpClient, ApplicationDbContext dbContext)
        {
            _httpClient = httpClient;
            _dbContext = dbContext;
        }

        public async Task<WeatherForecastDto?> GetWeatherAsync(string name, double lat, double lon)
        {

            if (name == null)
            {
                return null;
            }

            string url = $"https://api.openweathermap.org/data/3.0/onecall?lat={lat}&lon={lon}&exclude=minutely,hourly,alerts&units=metric&appid={_apiKey}";
            //var response = await _httpClient.GetAsync(url); // Get raw HTTP response
            //var jsonResponse = await response.Content.ReadAsStringAsync(); // Convert response to string

            //Console.WriteLine("OpenWeather API Response:");
            //Console.WriteLine(jsonResponse); // ✅ Print raw JSON to console

            var weatherData = await _httpClient.GetFromJsonAsync<WeatherForecastDto>(url);
            //WeatherForecastDto? weatherData = null;
            if (weatherData != null)
            {


                Console.WriteLine("Weather Forecast:");
                foreach (var day in weatherData.Daily)
                {
                    string date = DateTimeOffset.FromUnixTimeSeconds(day.DateTimestamp).ToString("yyyy-MM-dd");
                    Console.WriteLine($" {date} |  Min: {day.Temp.Min}°C - Max: {day.Temp.Max}°C |  Wind: {day.WindSpeed} km/h | {day.Weather[0].Description}");
                }

                var existingForecast = await _dbContext.WeatherForecasts
                .Include(w => w.DailyForecasts)
                .FirstOrDefaultAsync(w => w.Name == name && w.Latitude == lat && w.Longitude == lon);

                if (existingForecast != null)
                {
                    Console.WriteLine("Weather data already exists in the database.");

                    // Overwrite the existing daily forecast
                    existingForecast.DailyForecasts.Clear(); // Clear existing daily forecasts


                    foreach (var day in weatherData.Daily)
                    {

                        existingForecast.DailyForecasts.Add(new DailyForecast
                        {
                            DateTimestamp = day.DateTimestamp,
                            MinTemp = day.Temp.Min,
                            MaxTemp = day.Temp.Max,
                            Humidity = day.Humidity,
                            WindSpeed = day.WindSpeed,
                            WindDeg = day.WindDeg,
                            UvIndex = day.UvIndex,
                            Rain = day.Rain ?? 0,
                            Description = day.Weather[0].Description
                        });
                    }

                    // Update current weather
                    existingForecast.CurrentWeather = new CurrentWeather
                    {
                        Timestamp = weatherData.Current.Timestamp,
                        Sunrise = weatherData.Current.Sunrise,
                        Sunset = weatherData.Current.Sunset,
                        Temperature = weatherData.Current.Temperature,
                        FeelsLike = weatherData.Current.FeelsLike,
                        Pressure = weatherData.Current.Pressure,
                        Humidity = weatherData.Current.Humidity,
                        DewPoint = weatherData.Current.DewPoint,
                        UvIndex = weatherData.Current.UvIndex,
                        CloudCover = weatherData.Current.CloudCover,
                        Visibility = weatherData.Current.Visibility,
                        WindSpeed = weatherData.Current.WindSpeed,
                        WindDeg = weatherData.Current.WindDeg,
                        WindGust = weatherData.Current.WindGust,
                        Weather = weatherData.Current.Weather[0].Description
                    };
                }
                else
                {
                    var forecastEntity = new WeatherForecast
                    {
                        Name = name,
                        Latitude = lat,
                        Longitude = lon,
                        DailyForecasts = weatherData.Daily.Select(day => new DailyForecast
                        {
                            DateTimestamp = day.DateTimestamp,
                            MinTemp = day.Temp.Min,
                            MaxTemp = day.Temp.Max,
                            Humidity = day.Humidity,
                            WindSpeed = day.WindSpeed,
                            WindDeg = day.WindDeg,
                            UvIndex = day.UvIndex,
                            Rain = day.Rain ?? 0,
                            Description = day.Weather[0].Description
                        }).ToList(),

                        CurrentWeather = new CurrentWeather
                        {
                            Timestamp = weatherData.Current.Timestamp,
                            Sunrise = weatherData.Current.Sunrise,
                            Sunset = weatherData.Current.Sunset,
                            Temperature = weatherData.Current.Temperature,
                            FeelsLike = weatherData.Current.FeelsLike,
                            Pressure = weatherData.Current.Pressure,
                            Humidity = weatherData.Current.Humidity,
                            DewPoint = weatherData.Current.DewPoint,
                            UvIndex = weatherData.Current.UvIndex,
                            CloudCover = weatherData.Current.CloudCover,
                            Visibility = weatherData.Current.Visibility,
                            WindSpeed = weatherData.Current.WindSpeed,
                            WindDeg = weatherData.Current.WindDeg,
                            WindGust = weatherData.Current.WindGust,
                            Weather = weatherData.Current.Weather[0].Description
                        }
                    };

                    _dbContext.WeatherForecasts.Add(forecastEntity);

                }

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
                    Name = f.Name,
                    Latitude = f.Latitude,
                    Longitude = f.Longitude
                })
                .Distinct()
                .OrderBy(location => location.Name)
                .ToListAsync();

            return locations;
        }
    }

}
