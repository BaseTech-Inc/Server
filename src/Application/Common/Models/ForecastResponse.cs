using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Models
{
    public class ForecastResponse
    {
        public CoordForecast coord { get; set; }

        public WeatherForecast weather { get; set; }

        public MainForecast main { get; set; }

        public string q { get; set; }
    }

    public class CoordForecast
    {
        public double lon { get; set; }

        public double lat { get; set; }
    }

    public class WeatherForecast
    {
        public string main { get; set; }

        public string description { get; set; }

        public string icon { get; set; }
    }

    public class MainForecast
    {
        public float temp { get; set; }

        public float feels_like { get; set; }

        public float temp_min { get; set; }

        public float temp_max { get; set; }

        public int humidity { get; set; }
    }
}
