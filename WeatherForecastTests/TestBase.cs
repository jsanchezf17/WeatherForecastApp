using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
//using NUnit.Framework;
using System.Runtime.CompilerServices;
using WeatherForecastApp.Dal;
using WeatherForecastApp.Models;
using WeatherForecastApp.Repos;
using WeatherForecastApp.Validators;

[TestClass]
public class ControllerTestBase
{
    protected WebApplicationFactory<Program> Factory;
    protected IServiceProvider ServiceProvider;
    private readonly HttpClient _client;

    [TestInitialize]
    public void Initialize()
    {
        Factory = new WebApplicationFactory<Program>();

        var services = new ServiceCollection();

        // Add any test-specific services or configure services as needed
        //Add Services Pertaining to the weather app
        services.AddLogging();
        services.AddAutoMapper(typeof(MapProfile));
        services.AddDbContext<WeatherForecastDbContext>();
        services.AddScoped<ICoordinatesValidator, CoordinatesValidator>();
        services.AddScoped<IWeatherForecastService, WeatherForecastService>();
        services.AddTransient<IWeatherForecastDal, WeatherForecastDal>();
        services.AddScoped<IWeatherForecastRepo, WeatherForecastRepo>();
        services.AddScoped<ICoordinatesRepo, CoordinatesRepo>();
        


        // Build the service provider for the test scope
        ServiceProvider = services.BuildServiceProvider();
    }


    [TestCleanup]
    public void Cleanup()
    {
        (ServiceProvider as IDisposable)?.Dispose();
    }
}