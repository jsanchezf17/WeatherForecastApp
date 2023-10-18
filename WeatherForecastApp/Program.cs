using WeatherForecastApp.Dal;
using WeatherForecastApp.Models;
using WeatherForecastApp.Repos;
using WeatherForecastApp.Validators;
using AutoMapper;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);


//Add Services Pertaining to the weather app
builder.Services.AddDbContext<WeatherForecastDbContext>();
builder.Services.AddScoped<ICoordinatesValidator, CoordinatesValidator>();
builder.Services.AddScoped<IWeatherForecastService,WeatherForecastService>();
builder.Services.AddTransient<IWeatherForecastDal,WeatherForecastDal>();
builder.Services.AddScoped<IWeatherForecastRepo,WeatherForecastRepo>();
builder.Services.AddScoped<ICoordinatesRepo, CoordinatesRepo>();

builder.Services.AddAutoMapper(typeof(MapProfile));


// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(c =>
{
c.SwaggerDoc("v1", new OpenApiInfo { Title = "Weather Forecast API", Version = "v1", Description = "Ths application allows to save coordinate pairs to retrieve weather forecast data via stored data in DB or API using https://open-meteo.com" });
    
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
