using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WeatherForecastApp.Models;

public partial class WeatherForecastByCoordinatesDb
{
    public int ForecastId { get; set; }

    public int CoordinateId { get; set; }

    public DateTime Date { get; set; }

    public string TimeZone { get; set; } = null!;

    public double? Elevation { get; set; }

    public string? TemperatureUnit { get; set; }

    public string PrecipitationUnit { get; set; } = null!;

    public string HumidityUnit { get; set; } = null!;

    public DateTime CurrentTime { get; set; }

    public decimal CurrentTemperature { get; set; }

    public decimal CurrentPrecipitation { get; set; }

    public int CurrentRelativeHumidity { get; set; }

    public int? CurrentInterval { get; set; } 

    public virtual Coordinate Coordinate { get; set; } = null!;
}
