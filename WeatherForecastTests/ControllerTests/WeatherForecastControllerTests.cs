
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
    public class WeatherForecastControllerTests : ControllerTestBase
    {

        [TestMethod]
        public async Task GetForecastControllerTest() 
        {
            var logger = ServiceProvider.GetRequiredService<ILogger<WeatherForecastController>>();
            var coordinatesRepo = ServiceProvider.GetRequiredService<ICoordinatesRepo>();
            var weatherForecastRepo = ServiceProvider.GetRequiredService<IWeatherForecastRepo>();
            var coordinatesValidator = ServiceProvider.GetRequiredService<ICoordinatesValidator>();

            var controller = new WeatherForecastController(logger, coordinatesValidator, weatherForecastRepo, coordinatesRepo);

            Random random = new Random();
            double lat = random.NextDouble() * 90;
            double lng = random.NextDouble() * 180;


            IActionResult postResult = await controller.UpdateForecast(lat, lng);
            Assert.IsNotNull(postResult);

            IActionResult getResult = await controller.GetForecastAsync(lat, lng);
            Assert.IsNotNull(getResult);
        }

        [TestMethod]
        public async Task DeleteForecastControllerTest() 
        {
            var logger = ServiceProvider.GetRequiredService<ILogger<WeatherForecastController>>();
            var coordinatesRepo = ServiceProvider.GetRequiredService<ICoordinatesRepo>();
            var weatherForecastRepo = ServiceProvider.GetRequiredService<IWeatherForecastRepo>();
            var coordinatesValidator = ServiceProvider.GetRequiredService<ICoordinatesValidator>();

            var controller = new WeatherForecastController(logger, coordinatesValidator, weatherForecastRepo, coordinatesRepo);

            Random random = new Random();
            double lat = random.NextDouble() * 90;
            double lng = random.NextDouble() * 180;


            IActionResult postResult = await controller.UpdateForecast(lat, lng);
            Assert.IsNotNull(postResult);

            IActionResult getResult = await controller.GetForecastAsync(lat, lng);
            Assert.IsNotNull(getResult);

            IActionResult deleteResult = controller.Delete(lat, lng); 
            Assert.IsNotNull(deleteResult); 
        }

        [TestMethod]
        public async Task UpdateForecastNegativeControllerTest()  
        {
            var logger = ServiceProvider.GetRequiredService<ILogger<WeatherForecastController>>();
            var coordinatesRepo = ServiceProvider.GetRequiredService<ICoordinatesRepo>();
            var weatherForecastRepo = ServiceProvider.GetRequiredService<IWeatherForecastRepo>();
            var coordinatesValidator = ServiceProvider.GetRequiredService<ICoordinatesValidator>();

            var controller = new WeatherForecastController(logger, coordinatesValidator, weatherForecastRepo, coordinatesRepo);

            double lat = 500;
            double lng = 500;
            try
            {
                IActionResult postResult = await controller.UpdateForecast(lat, lng);
                Assert.IsNotNull(postResult);
            }
            catch (Exception e)
            {
                //Should be caught
                Console.WriteLine(e.Message);
            }
            
        }

        }
    }
