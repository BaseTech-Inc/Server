using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.GeoJson.Geometry
{
    public interface IPosition
    {
        double? Altitude { get; }
        double Latitude { get; }
        double Longitude { get; }
    }
}
