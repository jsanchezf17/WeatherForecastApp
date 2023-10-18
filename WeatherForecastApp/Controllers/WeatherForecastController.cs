using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using WeatherForecastApp.Repos;
using WeatherForecastApp.Validators;

namespace WeatherForecastApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {        

        private readonly ILogger<WeatherForecastController> _logger;
        private ICoordinatesValidator _coordinatesValidator;
        private IWeatherForecastRepo _forecastRepo;
        private ICoordinatesRepo _coordinatesRepo;
        public WeatherForecastController(ILogger<WeatherForecastController> logger, ICoordinatesValidator coordinatesValidator, IWeatherForecastRepo weatherForecastRepo, ICoordinatesRepo coordinatesRepo)
        {
            _logger = logger;
            _coordinatesValidator = coordinatesValidator;
            _forecastRepo = weatherForecastRepo;
            _coordinatesRepo = coordinatesRepo;
        }

        /// <summary>
        /// Gets the most current Weather Forecast based on Coordinates (latitude/longitude) input
        /// </summary>
        /// <param name="longitude"></param>
        /// <param name="latitude"></param>
        /// <returns></returns>
        [HttpGet(Name = "Forecast")]
        public async Task<IActionResult> GetForecastAsync(double latitude, double longitude) 
        {
            //Validate Coordinates are within the acceptable range
            _coordinatesValidator.ValidateCoordinates(latitude, longitude);
            //Get the Coordinate Pair for the Provided Coordinates
            //var coordinate = _coordinatesRepo.GetCoordinatePairByLatLng(latitude, longitude);

            //if (coordinate == null)
            //{
            //    return NotFound($"The Coordinate Pair ({latitude},{longitude}) was not found");
            //}

            //Get the Weather Forecast for the Provided Coordinates
            var weather = await _forecastRepo.GetCurrentWeatherForecast(latitude, longitude);
            if (weather == null)
            {
                return NotFound($"The weather forecast for the Coordinate Pair ({latitude},{longitude}) was not found");
            }

            return Ok(weather);
        }

        /// <summary>
        /// Updates/Creates weather forecast on DB Based on Coordinates (latitude/longitude) input
        /// </summary>
        /// <param name="longitude"></param>
        /// <param name="latitude"></param>
        /// <returns></returns>
        [HttpPut(Name = "Forecast")]
        public async Task<IActionResult> UpdateForecast(double latitude, double longitude)  
        {
            //Validate Coordinates are within the acceptable range
            _coordinatesValidator.ValidateCoordinates(latitude, longitude);            

            //Get the Weather Forecast for the Provided Coordinates
            var weather = await _forecastRepo.UpdateWeatherForecast(latitude, longitude);

            return Ok(weather);
        }

        /// <summary>
        /// Creates a new weather forecast entry on DB Based on Coordinates
        /// </summary>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        /// <returns></returns>
        [HttpPost(Name = "Forecast")]
        public async Task<IActionResult> PostForecast(double latitude, double longitude) 
        {
            //Validate Coordinates are within the acceptable range
            _coordinatesValidator.ValidateCoordinates(latitude, longitude);

            //Get the Weather Forecast for the Provided Coordinates
            var existingForecast = await _forecastRepo.CheckExistingWeatherForecast(latitude, longitude);
            if (existingForecast != null) { return Conflict($"The Weather Forecast for the Following Coordinates ({latitude},{longitude} already exist. Please use PUT call to update values"); };

            //Get the Weather Forecast for the Provided Coordinates
            var weather = await _forecastRepo.CreateWeatherForecast(latitude, longitude);
            return Ok(weather);
        }


        /// <summary>
        /// Deletes Saved Weather Forecast if it already exist in DB Based on Coordinates (latitude/longitude) input
        /// </summary>
        /// <param name="longitude"></param>
        /// <param name="latitude"></param>
        /// <returns></returns>
        [HttpDelete(Name = "Forecast")]
        public IActionResult Delete(double latitude, double longitude)
        {
            //Validate Coordinates are within the acceptable range
            _coordinatesValidator.ValidateCoordinates(latitude, longitude);
            //Get the Coordinate Pair for the Provided Coordinates
            var coordinate = _coordinatesRepo.GetCoordinatePairByLatLng(latitude, longitude);

            if (coordinate == null)
            {
                return NotFound($"The Coordinate Pair ({latitude},{longitude}) was not found");
            }
            _forecastRepo.DeleteWeatherForecast(latitude, longitude);

            return Ok();
        }
    }
}