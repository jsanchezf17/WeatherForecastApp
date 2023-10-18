using Models.APIs;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace WeatherForecastApp.Dal
{
    public class WeatherForecastService : IWeatherForecastService 
    {
        public async Task<OpenMeteoWeatherForecast> GetCurrentWeatherAsync(double lat, double lng)
        {
            OpenMeteoWeatherForecast weatherData = new OpenMeteoWeatherForecast();

            //Info Needed to Construct API Ccall
            //TODO Move URL to Settings
            string apiUrl = "https://api.open-meteo.com/v1/forecast";
            string queryParams = "current=temperature_2m,relativehumidity_2m,precipitation";

            // Construct the full URL with query parameters
            string fullUrl = $"{apiUrl}?latitude={lat}&longitude={lng}&{queryParams}";

            using (HttpClient httpClient = new HttpClient())
            {
                
                try
                {
                    // Make the GET request
                    HttpResponseMessage response = await httpClient.GetAsync(fullUrl);

                    // Check if the request was successful (status code 200)
                    if (response.IsSuccessStatusCode)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        Console.WriteLine("API Response:");
                        Console.WriteLine(apiResponse);

                        weatherData = JsonSerializer.Deserialize<OpenMeteoWeatherForecast>(apiResponse);
                    }
                    else
                    {
                        Console.WriteLine($"API Request failed with status code: {response.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"API Request failed with an exception: {ex.Message}");
                }
            }

            return weatherData;
        }
    }
}
