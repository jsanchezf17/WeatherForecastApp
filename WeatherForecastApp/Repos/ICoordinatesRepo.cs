using Models.Controllers;
using WeatherForecastApp.Models;

namespace WeatherForecastApp.Repos
{
    public interface ICoordinatesRepo
    {
        public Coordinate AddCoordinatePair(double lat, double lng);
        public void DeleteCoordinatePair(double lat, double lng);
        public Coordinate GetCoordinatePairByLatLng(double lat, double lng); 

        public List<Coordinate> GetAllCoordinatePairs(); 
    }
}
