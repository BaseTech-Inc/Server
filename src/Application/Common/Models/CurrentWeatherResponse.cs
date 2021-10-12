using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Models
{
    public class CurrentWeatherResponse
    {
        public CoordCurrent Coord { get; set; }

        public WeatherCurrentWeather Weather { get; set; }

        public MainCurrentWeather Main { get; set; }

        public string q { get; set; }
    }

    public class CoordCurrent
    {
        public double Lon { get; set; }

        public double Lat { get; set; }
    }

    public class WeatherCurrentWeather
    {
        public string Main { get; set; }

        public string Description { get; set; }

        public string Icon { get; set; }
    }

    public class MainCurrentWeather
    {
        public float Temp { get; set; }

        public float Feels_like { get; set; }

        public float Temp_min { get; set; }

        public float Temp_max { get; set; }

        public int Humidity { get; set; }
    }
}
