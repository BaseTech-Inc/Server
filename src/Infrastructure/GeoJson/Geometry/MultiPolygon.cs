using Infrastructure.GeoJson.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.GeoJson.Geometry
{
    public class MultiPolygon : GeoJSONObject, IGeometryObject
    {
        public MultiPolygon(IEnumerable<Polygon> polygons)
        {
            Coordinates = new ReadOnlyCollection<Polygon>(
                polygons?.ToArray() ?? throw new ArgumentNullException(nameof(polygons)));
        }

        [JsonConstructor]
        public MultiPolygon(IEnumerable<IEnumerable<IEnumerable<IEnumerable<double>>>> coordinates)
            : this(coordinates?.Select(polygon => new Polygon(polygon))
                   ?? throw new ArgumentNullException(nameof(coordinates)))
        {
        }

        public override GeoJSONObjectType Type => GeoJSONObjectType.MultiPolygon;

        [JsonProperty("coordinates", Required = Required.Always)]
        [JsonConverter(typeof(PolygonEnumerableConverter))]
        public ReadOnlyCollection<Polygon> Coordinates { get; }
    }
}
