using Microsoft.Extensions.DependencyInjection;
//using NUnit.Framework;
using WeatherForecastApp.Dal;
namespace WeatherForecastTests.RepoTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WeatherForecastApp.Repos;

[TestClass]
public class CoordinatesRepoTests : ControllerTestBase 
{
    [TestMethod]
    public async Task AddCoordinatePairTest() 
    {
        Random random = new Random();
        double lat = random.NextDouble() * 90;
        double lng = random.NextDouble() * 180;
        // Arrange
        var coordinatesRepo = ServiceProvider.GetRequiredService<ICoordinatesRepo>();
        //Create A weather Forecast for a coordinate pair 
        var createResponse = coordinatesRepo.AddCoordinatePair(lat, lng);
        //Read for the same coordinate Pair
        var coordinatesGetResponse = coordinatesRepo.GetCoordinatePairByLatLng(lat, lng);

        //Assert that the response is not null
        Assert.IsNotNull(coordinatesGetResponse);

        //Assert that the temperature response is between -100 and 200 
        Assert.IsTrue(coordinatesGetResponse.Latitude == (decimal)Math.Round(lat, 2) && coordinatesGetResponse.Longitude == (decimal)Math.Round(lng, 2));
    }

    [TestMethod]
    public async Task DeleteCoordinatePairTest()
    {
        Random random = new Random();
        double lat = random.NextDouble() * 90;
        double lng = random.NextDouble() * 180;
        // Arrange
        var coordinatesRepo = ServiceProvider.GetRequiredService<ICoordinatesRepo>();
        //Create A weather Forecast for a coordinate pair 
        var createResponse = coordinatesRepo.AddCoordinatePair(lat, lng);
        //Read for the same coordinate Pair
        var coordinatesGetResponse = coordinatesRepo.GetCoordinatePairByLatLng(lat, lng);
        //Assert that the response is not null
        Assert.IsNotNull(coordinatesGetResponse);

        //Delete Coordinate Pair
        coordinatesRepo.DeleteCoordinatePair(lat, lng);

        //Try Getting Response again
        coordinatesGetResponse = coordinatesRepo.GetCoordinatePairByLatLng(lat, lng);

        Assert.IsNull(coordinatesGetResponse);
    }

    [TestMethod]
    public async Task GetAllCoordinatePairsTest() 
    {
        Random random = new Random();
        double lat = random.NextDouble() * 90;
        double lng = random.NextDouble() * 180;
        // Arrange
        var coordinatesRepo = ServiceProvider.GetRequiredService<ICoordinatesRepo>();
        //Create A weather Forecast for a coordinate pair 
        var createResponse = coordinatesRepo.GetAllCoordinatePairs();
        Assert.IsNotNull(createResponse);
        
    }

    [TestMethod]
    public async Task AddCoordinatePairNegativeTest()
    {
       
        double lat = 300;
        double lng = 300;
        // Arrange
        var coordinatesRepo = ServiceProvider.GetRequiredService<ICoordinatesRepo>();
        //Create A weather Forecast for a coordinate pair 
        var createResponse = coordinatesRepo.GetCoordinatePairByLatLng(lat, lng);
        
        //Should return null for the incorrect values
        Assert.IsNull (createResponse);
       
    }


}