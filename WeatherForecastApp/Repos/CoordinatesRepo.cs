using AutoMapper;
using Models.Controllers;
using WeatherForecastApp.Dal;
using WeatherForecastApp.Models;

namespace WeatherForecastApp.Repos
{
    public class CoordinatesRepo : ICoordinatesRepo
    {
        private readonly ILogger<CoordinatesRepo> _logger;
        private IWeatherForecastDal _weatherForecastDal;
        private readonly IMapper _mapper;

        public CoordinatesRepo(ILogger<CoordinatesRepo> logger, IWeatherForecastDal weatherForecastDal, IMapper mapper)
        {
            _logger = logger;
            _weatherForecastDal = weatherForecastDal;
            _mapper = mapper;
        }

        public Coordinate AddCoordinatePair(double lat, double lng)
        {
            try
            {
                var coord = _weatherForecastDal.SaveCoordinatePair(lat, lng);
                return coord;
            }
            catch (Exception e)
            {
                throw new Exception($"There was an error adding coordinate pair to DB. Error: {e.Message}");
            }
        }

        
        /// <summary>
        /// Deletes Coordinate
        /// </summary>
        /// <param name="lat"></param>
        /// <param name="lng"></param>
        /// <exception cref="Exception"></exception>
        public void DeleteCoordinatePair(double lat, double lng)
        {
            try
            {

                //Find Coordinate by LAT/LNG
                var coord = _weatherForecastDal.GetCoordinateByLatLng(lat, lng);
                if (coord != null)
                {
                    _weatherForecastDal.DeleteWeatherForecastByCoordinateId(coord);
                    _weatherForecastDal.DeleteCoordinatePair(coord);
                }
            }
            catch (Exception e)
            {
                throw new Exception($"There was an error deleting coordinate pair to DB. Error: {e.Message}");
            }
        }

        /// <summary>
        /// Tries to Find the Coordinate pair associated with the LAT/LNG
        /// </summary>
        /// <param name="lat"></param>
        /// <param name="lng"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public Coordinate GetCoordinatePairByLatLng(double lat, double lng)
        {
            Coordinate coordinate = new Coordinate();
            try
            {
                using (var context = new WeatherForecastDbContext())
                {
                    coordinate = context.Coordinates.
                        Where(x => x.Latitude == (decimal)lat && x.Longitude == (decimal)lng).
                        FirstOrDefault();
                }
                return coordinate;
            }
            catch (Exception e)
            {
                throw new Exception($"There was an error getting Coordinate from DB. Error Message:{e.Message}");
            }
        }

        public List<Coordinate> GetAllCoordinatePairs()
        {
            return _weatherForecastDal.GetAllCoordinates();
        }
    }
}
