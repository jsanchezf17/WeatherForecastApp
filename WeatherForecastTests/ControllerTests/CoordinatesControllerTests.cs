
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherForecastApp.Controllers;
using WeatherForecastApp.Repos;
using WeatherForecastApp.Validators;

namespace WeatherForecastTests.ControllerTests
{
    [TestClass]
    public class CoordinatesControllerTests : ControllerTestBase 
    {

        [TestMethod]
        public async Task GetCoordinateControllerTest()
        {
            var logger = ServiceProvider.GetRequiredService<ILogger<CoordinatesController>>();
            var coordinatesRepo = ServiceProvider.GetRequiredService<ICoordinatesRepo>();
            var weatherForecastRepo = ServiceProvider.GetRequiredService<IWeatherForecastRepo>();
            var coordinatesValidator = ServiceProvider.GetRequiredService<ICoordinatesValidator>();

            var controller = new CoordinatesController(logger, coordinatesValidator, weatherForecastRepo, coordinatesRepo);

            Random random = new Random();
            double lat = random.NextDouble() * 90;
            double lng = random.NextDouble() * 180;


            IActionResult postResult = controller.PostCoordinates(lat, lng);
            Assert.IsNotNull(postResult);

            IActionResult getResult = controller.GetCoordinates(lat, lng);
            Assert.IsNotNull(getResult);
        }

        [TestMethod]
        public async Task GetCoordinateNegativeControllerTest()
        {
            var logger = ServiceProvider.GetRequiredService<ILogger<CoordinatesController>>();
            var coordinatesRepo = ServiceProvider.GetRequiredService<ICoordinatesRepo>();
            var weatherForecastRepo = ServiceProvider.GetRequiredService<IWeatherForecastRepo>();
            var coordinatesValidator = ServiceProvider.GetRequiredService<ICoordinatesValidator>();

            var controller = new CoordinatesController(logger, coordinatesValidator, weatherForecastRepo, coordinatesRepo);

            double lat = 500;
            double lng = 500;

            try
            {
                IActionResult getResult = controller.GetCoordinates(lat, lng);
                Assert.IsNotNull(getResult);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                // Should be caught
            }
            
        }

        [TestMethod]
        public async Task GetAllCoordinatesControllerTest()
        {
            var logger = ServiceProvider.GetRequiredService<ILogger<CoordinatesController>>();
            var coordinatesRepo = ServiceProvider.GetRequiredService<ICoordinatesRepo>();
            var weatherForecastRepo = ServiceProvider.GetRequiredService<IWeatherForecastRepo>();
            var coordinatesValidator = ServiceProvider.GetRequiredService<ICoordinatesValidator>();

            var controller = new CoordinatesController(logger, coordinatesValidator, weatherForecastRepo, coordinatesRepo);


            IActionResult getResult = controller.GetAllCoordinates();
            Assert.IsNotNull(getResult);

        }

        [TestMethod]
        public async Task DeleteCoordinatesControllerTest()
        {
            var logger = ServiceProvider.GetRequiredService<ILogger<CoordinatesController>>();
            var coordinatesRepo = ServiceProvider.GetRequiredService<ICoordinatesRepo>();
            var weatherForecastRepo = ServiceProvider.GetRequiredService<IWeatherForecastRepo>();
            var coordinatesValidator = ServiceProvider.GetRequiredService<ICoordinatesValidator>();

            var controller = new CoordinatesController(logger, coordinatesValidator, weatherForecastRepo, coordinatesRepo);

            Random random = new Random();
            double lat = random.NextDouble() * 90;
            double lng = random.NextDouble() * 180;


            IActionResult postResult = controller.PostCoordinates(lat, lng);
            Assert.IsNotNull(postResult);

            IActionResult getResult = controller.GetCoordinates(lat, lng);
            Assert.IsNotNull(getResult);

            IActionResult deleteResult = controller.DeleteCoordinates(lat, lng);
            Assert.IsNotNull(deleteResult);

            getResult = controller.GetCoordinates(lat, lng);
            Assert.IsNotNull(getResult);

        }

        [TestMethod]
        public async Task DeleteCoordinatesNegativeControllerTest()
        {
            var logger = ServiceProvider.GetRequiredService<ILogger<CoordinatesController>>();
            var coordinatesRepo = ServiceProvider.GetRequiredService<ICoordinatesRepo>();
            var weatherForecastRepo = ServiceProvider.GetRequiredService<IWeatherForecastRepo>();
            var coordinatesValidator = ServiceProvider.GetRequiredService<ICoordinatesValidator>();

            var controller = new CoordinatesController(logger, coordinatesValidator, weatherForecastRepo, coordinatesRepo);

            Random random = new Random();
            double lat = random.NextDouble() * 90;
            double lng = random.NextDouble() * 180;

            IActionResult deleteResult = controller.DeleteCoordinates(lat, lng);
            Assert.IsNotNull(deleteResult);
        }
    }

}
