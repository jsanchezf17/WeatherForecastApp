using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WeatherForecastApp.Models;

public partial class Coordinate
{
    [JsonIgnore]
    public int CoordinateId { get; set; }

    public decimal Latitude { get; set; }

    public decimal Longitude { get; set; }

    [JsonIgnore]
    public virtual ICollection<WeatherForecastByCoordinatesDb> WeatherForecastByCoordinatesDbs { get; set; } = new List<WeatherForecastByCoordinatesDb>();
}
