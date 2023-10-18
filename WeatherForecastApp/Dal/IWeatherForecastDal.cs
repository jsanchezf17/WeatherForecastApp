using Models;
using WeatherForecastApp.Models;

namespace WeatherForecastApp.Dal
{
    public interface IWeatherForecastDal 
    {
        public void SaveWeatherForecast(WeatherForecastByCoordinatesDb forecast);
        public void UpdateWeatherForecast(WeatherForecastByCoordinatesDb forecast); 
        public Coordinate SaveCoordinatePair(double lat, double lng); 
        public void DeleteCoordinatePair(Coordinate coordinate);
        public WeatherForecastByCoordinatesDb GetWeatherForecastByLatLng(double lat, double lng);
        public WeatherForecastByCoordinatesDb GetWeatherForecastByCoordinateId(int coordinateId); 

        public Coordinate GetCoordinateByLatLng(double lat, double lng);

        public List<Coordinate> GetAllCoordinates();

        public void DeleteWeatherForecastByCoordinateId(Coordinate coordinatePair);
    }
}
