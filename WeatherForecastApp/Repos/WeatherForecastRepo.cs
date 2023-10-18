using AutoMapper;
using Models.APIs;
using Models.Controllers;
using WeatherForecastApp.Controllers;
using WeatherForecastApp.Dal;
using WeatherForecastApp.Models;
using WeatherForecastApp.Validators;

namespace WeatherForecastApp.Repos
{
    public class WeatherForecastRepo : IWeatherForecastRepo
    {
        private readonly ILogger<WeatherForecastRepo> _logger;
        private IWeatherForecastService _weatherForecastService;
        private IWeatherForecastDal _weatherForecastDal;
        private readonly IMapper _mapper;

        public WeatherForecastRepo(ILogger<WeatherForecastRepo> logger, IWeatherForecastDal weatherForecastDal, IWeatherForecastService weatherForecastService, IMapper mapper)
        {
            _logger = logger;
            _weatherForecastService = weatherForecastService;
            _weatherForecastDal = weatherForecastDal;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets the Current Weather Data for the Coordinate pair given if it already exists
        /// </summary>
        /// <param name="lat"></param>
        /// <param name="lng"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<WeatherForecastResponse> GetCurrentWeatherForecast(double lat, double lng)
        {
            //First Check if Weather Forecast Already Exists in DB based on Coordinates
            var coordinate = _weatherForecastDal.GetCoordinateByLatLng(lat, lng);
            if (coordinate == null)
            {
                coordinate = new Coordinate();
                //throw new Exception("The coordinates used don't have an existing record yet, please create new coordinates record first");
                coordinate.Latitude = (decimal)lat;
                coordinate.Longitude = (decimal)lng;
            }
            //If Current Data Already Exist in db return that weather data
            var dbWeatherData = _weatherForecastDal.GetWeatherForecastByCoordinateId(coordinate.CoordinateId);
            if (dbWeatherData == null)
            {
                //If DB does not contain weather forecast, get from api
                dbWeatherData = await GetAPIWeatherAndConvert(coordinate);
            }
            var responseObject = _mapper.Map<WeatherForecastResponse>(dbWeatherData);
            responseObject.coordinates = coordinate;   
            return responseObject;
        }

        /// <summary>
        /// Get the Current Weather Forecast from Open Meteo and converts it to DB Model
        /// </summary>
        /// <param name="coordinate"></param>
        /// <returns></returns>
        private async Task<WeatherForecastByCoordinatesDb> GetAPIWeatherAndConvert(Coordinate coordinate)
        {
            var weatherData = await _weatherForecastService.GetCurrentWeatherAsync((double)coordinate.Latitude, (double)coordinate.Longitude);
            //Save Weather Data from API into DB
            WeatherForecastByCoordinatesDb forecast = new WeatherForecastByCoordinatesDb
            {
                CoordinateId = coordinate.CoordinateId,
                Date = DateTime.Parse(weatherData.current.time),
                TimeZone = weatherData.timezone,
                Elevation = (float)weatherData.elevation,
                TemperatureUnit = weatherData.current_units.temperature_2m,
                PrecipitationUnit = weatherData.current_units.precipitation,
                HumidityUnit = weatherData.current_units.relativehumidity_2m,
                CurrentTime = DateTime.Parse(weatherData.current.time),
                CurrentTemperature = (decimal)weatherData.current.temperature_2m,
                CurrentPrecipitation = (decimal)weatherData.current.precipitation,
                CurrentRelativeHumidity = weatherData.current.relativehumidity_2m,
                CurrentInterval = weatherData.current.interval                
            };
            return forecast;
        }

        /// <summary>
        /// Updates/Creates the Weather forecast on for the given coordinate pair
        /// </summary>
        /// <param name="lat"></param>
        /// <param name="lng"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<WeatherForecastResponse> UpdateWeatherForecast(double lat, double lng)
        {         
            //First Check if Weather Forecast Already Exists in DB based on Coordinates
            var coordinate = _weatherForecastDal.GetCoordinateByLatLng(lat, lng);
            //If Coordinate does not exist in DB create a new entry
            if (coordinate == null)
            {
                coordinate = _weatherForecastDal.SaveCoordinatePair(lat, lng);

                // If Coordinate does not exist then Weather Data does not exist
                // Get weather data
                // Weather data does not exist, use open-meteo api to get updated data
                var dbWeatherData = await GetAPIWeatherAndConvert(coordinate);
                _weatherForecastDal.SaveWeatherForecast(dbWeatherData);
                var responseObject = _mapper.Map<WeatherForecastResponse>(dbWeatherData);
                responseObject.coordinates = coordinate;
                return responseObject;
            }
            else
            {
                
                var dbWeatherData = await GetAPIWeatherAndConvert(coordinate);

                _weatherForecastDal.SaveWeatherForecast(dbWeatherData);
                var responseObject = _mapper.Map<WeatherForecastResponse>(dbWeatherData);
                responseObject.coordinates = coordinate;
                return responseObject;
            }
        }

        /// <summary>
        /// Deletes current weather forecast by latitude and longitude
        /// </summary>
        /// <param name="lat"></param>
        /// <param name="lng"></param>
        /// <exception cref="Exception"></exception>
        public void DeleteWeatherForecast(double lat, double lng)
        {
            try
            {
                //Find Coordinate by LAT/LNG
                var coord = _weatherForecastDal.GetCoordinateByLatLng(lat, lng);
                if (coord != null)
                {
                    _weatherForecastDal.DeleteWeatherForecastByCoordinateId(coord);
                }
            }
            catch (Exception e)
            {
                throw new Exception($"There was an error deleting weather forecast DB. Error: {e.Message}");
            }
        }

        public async Task<WeatherForecastResponse> CreateWeatherForecast(double lat, double lng)
        {
            //First Check if Weather Forecast Already Exists in DB based on Coordinates
            var coordinate = _weatherForecastDal.GetCoordinateByLatLng(lat, lng);
            //If Coordinate does not exist in DB create a new entry
            if (coordinate == null)
            {
                coordinate = _weatherForecastDal.SaveCoordinatePair(lat, lng);                
            }

            var dbWeatherData = await GetAPIWeatherAndConvert(coordinate);
            _weatherForecastDal.SaveWeatherForecast(dbWeatherData);
            var responseObject = _mapper.Map<WeatherForecastResponse>(dbWeatherData);
            responseObject.coordinates = coordinate;
            return responseObject;

        }

        public async Task<WeatherForecastResponse> CheckExistingWeatherForecast(double lat, double lng)
        {
            //First Check if Weather Forecast Already Exists in DB based on Coordinates
            var coordinate = _weatherForecastDal.GetCoordinateByLatLng(lat, lng);
            if (coordinate == null)
            {
                coordinate = new Coordinate();
                //throw new Exception("The coordinates used don't have an existing record yet, please create new coordinates record first");
                coordinate.Latitude = (decimal)lat;
                coordinate.Longitude = (decimal)lng;
            }
            //If Current Data Already Exist in db return that weather data
            var dbWeatherData = _weatherForecastDal.GetWeatherForecastByCoordinateId(coordinate.CoordinateId);
            if (dbWeatherData == null)
            {
                return null;
            }
            var responseObject = _mapper.Map<WeatherForecastResponse>(dbWeatherData);
            responseObject.coordinates = coordinate;
            return responseObject;
        }
    }
}
