using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Forecast
{
    public class CurrentWeatherDto
    {
        public Coord Coord { get; set; }

        public IList<Weather> Weather { get; set; }

        public Main Main { get; set; }

        public string Name { get; set; }
    }

    public class Coord
    {
        public double Lon { get; set; }

        public double Lat { get; set; }
    }

    public class Weather
    {
        public string Main { get; set; }

        public string Description { get; set; }

        public string Icon { get; set; }
    }

    public class Main
    {
        public float Temp { get; set; }

        public float Feels_like { get; set; }

        public float Temp_min { get; set; }

        public float Temp_max { get; set; }

        public int Humidity { get; set; }
    }
}
