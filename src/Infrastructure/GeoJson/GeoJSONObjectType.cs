using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.GeoJson
{
    public enum GeoJSONObjectType
    {
        Point,
        MultiPoint,
        LineString,
        MultiLineString, 
        Polygon,
        MultiPolygon,
        GeometryCollection,
        Feature,
        FeatureCollection
    }
}
