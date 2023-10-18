using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using WeatherForecastApp.Models;
using WeatherForecastApp.Repos;
using WeatherForecastApp.Validators;

namespace WeatherForecastApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CoordinatesController : ControllerBase
    {

        private readonly ILogger<CoordinatesController> _logger;
        private ICoordinatesValidator _coordinatesValidator;
        private IWeatherForecastRepo _forecastRepo;
        private ICoordinatesRepo _coordinatesRepo;

        public CoordinatesController(ILogger<CoordinatesController> logger, ICoordinatesValidator coordinatesValidator, IWeatherForecastRepo weatherForecastRepo, ICoordinatesRepo coordinatesRepo)
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
        [HttpGet("{latitude}&{longitude}")]
        public IActionResult GetCoordinates(double latitude, double longitude)
        {
            //Validate Coordinates are within the acceptable range
            _coordinatesValidator.ValidateCoordinates(latitude, longitude);

            //Get the Coordinate Pair for the Provided Coordinates
            var coordinate = _coordinatesRepo.GetCoordinatePairByLatLng(latitude, longitude);

            if (coordinate == null)
            {
                return NotFound($"The Coordinate Pair ({latitude},{longitude}) was not found");
            }


            return Ok(coordinate);
        }


        /// <summary>
        /// Gets a list of all coordinate pairs stored in the DB
        /// Can use pageSize and currentPage for Pagination Purposes
        /// </summary>
        /// <returns></returns>
        [HttpGet(Name = "Coordinates")]
        public IActionResult GetAllCoordinates()
        {         
            //TODO Maybe some pagination could be useful if data set is too big

            //Get the Coordinate Pair for the Provided Coordinates
            var coordinates = _coordinatesRepo.GetAllCoordinatePairs();            

            return Ok(coordinates);
        }

        /// <summary>
        /// Updates/Creates Coordinates Entry (latitude/longitude) input
        /// </summary>
        /// <param name="longitude"></param>
        /// <param name="latitude"></param>
        /// <returns></returns>
        [HttpPost(Name = "Coordinates")]
        public IActionResult PostCoordinates(double latitude, double longitude)
        {
            //Validate Coordinates are within the acceptable range
            _coordinatesValidator.ValidateCoordinates(latitude, longitude);

            //Get the Coordinate Pair for the Provided Coordinates
            var coordinate = _coordinatesRepo.GetCoordinatePairByLatLng(latitude, longitude);
            if (coordinate != null)
            {
                return Conflict($"The coordinate ({latitude},{longitude}) already exists in the DB");
            }

            //Add A new Set of Coordinates
            var savedCoordinate = _coordinatesRepo.AddCoordinatePair(latitude, longitude);

            return Ok(savedCoordinate);
        }

        /// <summary>
        /// Deletes Coordinates Entry (latitude/longitude) input
        /// </summary>
        /// <param name="longitude"></param>
        /// <param name="latitude"></param>
        /// <returns></returns>
        [HttpDelete(Name = "Coordinates")]
        public IActionResult DeleteCoordinates(double latitude, double longitude)
        {
            //Validate Coordinates are within the acceptable range
            _coordinatesValidator.ValidateCoordinates(latitude, longitude);

            try
            {
                //Get the Coordinate Pair for the Provided Coordinates
                var coordinate = _coordinatesRepo.GetCoordinatePairByLatLng(latitude, longitude);

                if (coordinate == null)
                {
                    return NotFound($"The Coordinate Pair ({latitude},{longitude}) was not found");
                }

                _coordinatesRepo.DeleteCoordinatePair(latitude, longitude);
                return Ok(coordinate);
            }
            catch (Exception e)
            {

                return BadRequest($"There was an error deleting the requested resource. Error: {e.Message}");
            }

        }
    }
}