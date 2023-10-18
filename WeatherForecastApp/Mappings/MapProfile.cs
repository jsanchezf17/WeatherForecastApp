using AutoMapper;
using Models.APIs;
using Models.Controllers;
using WeatherForecastApp.Models;

public class MapProfile : Profile
{
    public MapProfile()
    {
        CreateMap<WeatherForecastByCoordinatesDb, WeatherForecastResponse>();
        CreateMap<OpenMeteoWeatherForecast, WeatherForecastByCoordinatesDb > ();
        // Add other mappings as needed
    }
}