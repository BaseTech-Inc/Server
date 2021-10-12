using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Models
{
    public class ForecastResponse
    {
        public string q { get; set; }

        public CoordForecast Coord { get; set; }

        public IList<Hourly> Hourly { get; set; }

        public IList<Daily> Daily { get; set; }
    }

    public class CoordForecast
    {
        public double Lon { get; set; }

        public double Lat { get; set; }
    }

    public class Hourly
    {
        public int Dt { get; set; }

        public float Temp { get; set; }

        public float Feels_like { get; set; }

        public int Humidity { get; set; }
    }

    public class Daily
    {
        public int Dt { get; set; }

        public PartDay Temp { get; set; }

        public PartDay Feels_like { get; set; }

        public int Humidity { get; set; }
    }

    public class PartDay
    {
        public float Day { get; set; }

        public float Night { get; set; }

        public float Eve { get; set; }

        public float Morn { get; set; }
    }
}
