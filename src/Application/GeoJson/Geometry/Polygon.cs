using Application.GeoJson.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Application.GeoJson.Geometry
{
    public class Polygon : GeoJSONObject, IGeometryObject
    {
        public Polygon(IEnumerable<LineString> coordinates)
        {
            Coordinates = new ReadOnlyCollection<LineString>(
                coordinates?.ToArray() ?? throw new ArgumentNullException(nameof(coordinates)));
            if (Coordinates.Any(linearRing => !linearRing.IsLinearRing()))
            {
                throw new ArgumentException("All elements must be closed LineStrings with 4 or more positions" +
                                            " (see GeoJSON spec at 'https://tools.ietf.org/html/rfc7946#section-3.1.6').", nameof(coordinates));
            }


        }

        [JsonConstructor]
        public Polygon(IEnumerable<IEnumerable<IEnumerable<double>>> coordinates)
            : this(coordinates?.Select(line => new LineString(line))
              ?? throw new ArgumentNullException(nameof(coordinates)))
        {
        }

        public override GeoJSONObjectType Type => GeoJSONObjectType.Polygon;

        [JsonProperty("coordinates", Required = Required.Always)]
        [JsonConverter(typeof(LineStringEnumerableConverter))]
        public ReadOnlyCollection<LineString> Coordinates { get; }
    }
}
