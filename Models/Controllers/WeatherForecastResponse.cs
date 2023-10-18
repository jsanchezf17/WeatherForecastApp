using Models.APIs;
using System.Text.Json.Serialization;
using WeatherForecastApp.Models;

namespace Models.Controllers
{

    public class WeatherForecastResponse
    {
        public Coordinate coordinates { get; set; }    

        public DateTime Date { get; set; }

        public string TimeZone { get; set; } = null!;

        public float? Elevation { get; set; }

        public string? TemperatureUnit { get; set; }

        public string PrecipitationUnit { get; set; } = null!;

        public string HumidityUnit { get; set; } = null!;

        public DateTime CurrentTime { get; set; }

        public decimal CurrentTemperature { get; set; }

        public decimal CurrentPrecipitation { get; set; }

        public int CurrentRelativeHumidity { get; set; }

    }
    

}