using Microsoft.Extensions.DependencyInjection;
//using NUnit.Framework;
using WeatherForecastApp.Dal;
namespace WeatherForecastTests.ServiceTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class WeatherForecastServiceTests : ControllerTestBase
{
    [TestMethod] 
    public async Task GetCurrentWeatherAsyncTest()
    {
        Random random = new Random();
        double lat = random.NextDouble() * 90;
        double lng = random.NextDouble() * 180;
        // Arrange
        var weatherForecastService = ServiceProvider.GetRequiredService<IWeatherForecastService>();
        var weatherServiceResponse = await weatherForecastService.GetCurrentWeatherAsync(lat,lng);

        Assert.AreEqual(weatherServiceResponse.GetType().Name, typeof(Models.APIs.OpenMeteoWeatherForecast).Name);
    }
}