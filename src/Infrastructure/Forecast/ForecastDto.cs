using Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Forecast
{
    public class ForecastDto
    {
        public double Lat { get; set; }

        public double Lon { get; set; }

        public IList<Hourly> Hourly { get; set; }

        public IList<Daily> Daily { get; set; }
    }
}
