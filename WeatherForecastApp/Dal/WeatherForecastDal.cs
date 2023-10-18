using Microsoft.EntityFrameworkCore;
using Models;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using WeatherForecastApp.Models;
using WeatherForecastApp.Repos;

namespace WeatherForecastApp.Dal
{
    public class WeatherForecastDal : IWeatherForecastDal
    {
        /// <summary>
        /// Tries to Find the Coordinate pair associated with the LAT/LNG
        /// </summary>
        /// <param name="lat"></param>
        /// <param name="lng"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public Coordinate GetCoordinateByLatLng(double lat, double lng)
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

        
        /// <summary>
        /// Saves a LAT/LNG coordinate pair to the DB
        /// </summary>
        /// <param name="lat"></param>
        /// <param name="lng"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public Coordinate SaveCoordinatePair(double lat, double lng)
        {
            Coordinate coordinate = new Coordinate
            {
                Latitude = (Decimal)lat,
                Longitude = (Decimal)lng
            };
            try
            {
                using (var context = new WeatherForecastDbContext())
                {
                    var savedCoordinate = context.Add(coordinate);
                    context.SaveChanges();
                    return coordinate;
                }               
            }
            catch (Exception e)
            {                
                throw new Exception($"There was an error saving new coordinate pair to DB. Error Message:{e.Message}");
            }
           
        }

        /// <summary>
        /// Deletes A Coordinate Pair from the DB
        /// </summary>
        /// <param name="coordinatePair"></param>
        /// <exception cref="Exception"></exception>
        public void DeleteCoordinatePair(Coordinate coordinatePair)
        {            
            try
            {
                using (var context = new WeatherForecastDbContext())
                {
                    var savedCoordinate = context.Remove(coordinatePair);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception($"There was an deleting coordinate pair to DB. Error Message:{e.Message}");
            }

        }


        public void DeleteWeatherForecastByCoordinateId(Coordinate coordinatePair) 
        {
            try
            {
                using (var context = new WeatherForecastDbContext())
                {
                    var weatherForecast = context.WeatherForecastByCoordinatesDbs.
                       Where(x => x.CoordinateId == coordinatePair.CoordinateId).ToList();

                    context.RemoveRange(weatherForecast);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception($"There was an deleting weatherForecast to DB. Error Message:{e.Message}");
            }
        }


        /// <summary>
        /// Saves a new Entry of Weather Forecast
        /// </summary>
        /// <param name="forecast"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public void SaveWeatherForecast(WeatherForecastByCoordinatesDb forecast)
        {
            try
            {
                using (var context = new WeatherForecastDbContext())
                {
                    context.Add(forecast);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception($"There was an error saving weather data to DB. Error Message:{e.Message}");
            }
            
        }

        /// <summary>
        /// Updates a Weather Forecast entry to the newest data
        /// </summary>
        /// <param name="forecast"></param>
        /// <exception cref="Exception"></exception>
        public void UpdateWeatherForecast(WeatherForecastByCoordinatesDb forecast) 
        {
            try
            {
                using (var context = new WeatherForecastDbContext())
                {
                    //Find Current Forecast to Update
                    var weatherForecast = context.WeatherForecastByCoordinatesDbs.
                      Where(x => x.ForecastId == forecast.ForecastId).
                      FirstOrDefault();

                    context.Add(forecast);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception($"There was an error saving weather data to DB. Error Message:{e.Message}");
            }

        }

        public WeatherForecastByCoordinatesDb GetWeatherForecastByLatLng(double lat, double lng)
        {

            Coordinate coordinate = GetCoordinateByLatLng(lat, lng);

            WeatherForecastByCoordinatesDb weatherForecast = new WeatherForecastByCoordinatesDb();
            try
            {
                using (var context = new WeatherForecastDbContext())
                {
                    weatherForecast = context.WeatherForecastByCoordinatesDbs.
                       Where(x => x.CoordinateId == coordinate.CoordinateId).
                       FirstOrDefault();                    
                }
                return weatherForecast;
            }
            catch (Exception e)
            {
                throw new Exception($"There was an error retrieving weather data with coordinate id. Error Message:{e.Message}");
            }
        }

        /// <summary>
        /// Get weather forecast Data from DB using coordinate id
        /// </summary>
        /// <param name="coordinateId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public WeatherForecastByCoordinatesDb GetWeatherForecastByCoordinateId(int coordinateId)
        {
            WeatherForecastByCoordinatesDb weatherForecast = new WeatherForecastByCoordinatesDb();
            try
            {
                using (var context = new WeatherForecastDbContext())
                {
                    weatherForecast = context.WeatherForecastByCoordinatesDbs.
                       Where(x => x.CoordinateId == coordinateId).
                       FirstOrDefault();
                }
                return weatherForecast;
            }
            catch (Exception e)
            {
                throw new Exception($"There was an error retrieving weather data with coordinate id. Error Message:{e.Message}");
            }
        }

        /// <summary>
        /// Retrieves all Coordinate Pairs Stored in the DB
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public List<Coordinate> GetAllCoordinates()
        {
            List<Coordinate> coordinates = new List<Coordinate>();
            try
            {
                using (var context = new WeatherForecastDbContext())
                {
                    coordinates = context.Coordinates.ToList();
                }
                return coordinates;
            }
            catch (Exception e)
            {
                throw new Exception($"There was an error retrieving all coordinates. Error Message:{e.Message}");
            }
        }
    }
}
