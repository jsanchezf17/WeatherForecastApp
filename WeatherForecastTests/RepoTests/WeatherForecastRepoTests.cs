using Microsoft.Extensions.DependencyInjection;
//using NUnit.Framework;
using WeatherForecastApp.Dal;
namespace WeatherForecastTests.RepoTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WeatherForecastApp.Repos;

[TestClass]
public class WeatherForecastRepoTests : ControllerTestBase
{
    [TestMethod]
    public async Task GetCurrentWeatherForecastAsyncTest()
    {
        Random random = new Random();
        double lat = random.NextDouble() * 90;
        double lng = random.NextDouble() * 180;
        // Arrange
        var weatherForecastRepo = ServiceProvider.GetRequiredService<IWeatherForecastRepo>();
        //Create A weather Forecast for a coordinate pair 
        var createRespone = await weatherForecastRepo.UpdateWeatherForecast(lat, lng);
        //Read for the same coordinate Pair
        var weatherForecastRepoResponse = await weatherForecastRepo.GetCurrentWeatherForecast(lat, lng);

        //Assert that the response is not null
        Assert.IsNotNull(weatherForecastRepoResponse);

        //Assert that the temperature response is between -100 and 200 
        Assert.IsTrue(weatherForecastRepoResponse.CurrentTemperature >= -100 && weatherForecastRepoResponse.CurrentTemperature <= 200);
    }

    [TestMethod]
    public async Task GetCurrentWeatherForecastAsyncTestNegative()
    {
        try
        {


            double lat = 320;
            double lng = 320;
            // Arrange
            var weatherForecastRepo = ServiceProvider.GetRequiredService<IWeatherForecastRepo>();

            //Read for the same coordinate Pair
            var weatherForecastRepoResponse = await weatherForecastRepo.GetCurrentWeatherForecast(lat, lng);


        }
        catch (Exception)
        {
            //There should be an error when running this
        }
    }

    [TestMethod]
    public async Task DeleteWeatherForecastAsyncTest()
    {
        Random random = new Random();
        double lat = random.NextDouble() * 90;
        double lng = random.NextDouble() * 180;
        // Arrange
        var weatherForecastRepo = ServiceProvider.GetRequiredService<IWeatherForecastRepo>();
        //Create A weather Forecast for a coordinate pair 
        var createResponse = await weatherForecastRepo.UpdateWeatherForecast(lat, lng);
        //Read for the same coordinate Pair
        weatherForecastRepo.DeleteWeatherForecast(lat, lng);

        //Assert that the response is not null
        Assert.IsNotNull(createResponse);

    }

    [TestMethod]
    public async Task UpdateCurrentWeatherForecastAsyncTest()
    {
        Random random = new Random();
        double lat = random.NextDouble() * 90;
        double lng = random.NextDouble() * 180;
        // Arrange
        var weatherForecastRepo = ServiceProvider.GetRequiredService<IWeatherForecastRepo>();
        //Create A weather Forecast for a coordinate pair 
        var createRespone = await weatherForecastRepo.UpdateWeatherForecast(lat, lng);
        // Run update again to trigger logic
        createRespone = await weatherForecastRepo.UpdateWeatherForecast(lat, lng);
        //Read for the same coordinate Pair
        var weatherForecastRepoResponse = await weatherForecastRepo.GetCurrentWeatherForecast(lat, lng);

        //Assert that the response is not null
        Assert.IsNotNull(weatherForecastRepoResponse);

        //Assert that the temperature response is between -100 and 200 
        Assert.IsTrue(weatherForecastRepoResponse.CurrentTemperature >= -100 && weatherForecastRepoResponse.CurrentTemperature <= 200);
    }
}