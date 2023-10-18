using Models.APIs;

namespace WeatherForecastApp.Dal
{
    public interface IWeatherForecastService  
    {
        public Task<OpenMeteoWeatherForecast> GetCurrentWeatherAsync(double lat, double lng);
    }
}
