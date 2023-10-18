using Models.APIs;
using Models.Controllers;
using WeatherForecastApp.Models;

namespace WeatherForecastApp.Repos
{
    public interface IWeatherForecastRepo  
    {
        public Task<WeatherForecastResponse> CheckExistingWeatherForecast(double lat, double lng);  
        public Task<WeatherForecastResponse> GetCurrentWeatherForecast(double lat, double lng); 
        public Task<WeatherForecastResponse> UpdateWeatherForecast(double lat, double lng);
        public Task<WeatherForecastResponse> CreateWeatherForecast(double lat, double lng); 
        public void DeleteWeatherForecast(double lat, double lng); 

    }
}
