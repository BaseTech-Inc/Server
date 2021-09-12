using Application.Common.Models;
using Application.GeoJson.Features;
using Application.GeoJson.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IGooglePointService
    {
        IEnumerable<CoordinateEntity> DecodeCoordinates(string encodedPoints);

        string EncodeGeojson(Feature<LineString> points);

        string EncodeCoordinate(IEnumerable<CoordinateEntity> points);
    }
}
