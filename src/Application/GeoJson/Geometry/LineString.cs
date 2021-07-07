using Application.GeoJson.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Application.GeoJson.Geometry
{
    public class LineString : GeoJSONObject, IGeometryObject
    {
        [JsonConstructor]
        public LineString(IEnumerable<IEnumerable<double>> coordinates)
        : this(coordinates?.Select(latLongAlt => (IPosition)latLongAlt.ToPosition())
               ?? throw new ArgumentException(nameof(coordinates)))
        {
        }

        public LineString(IEnumerable<IPosition> coordinates)
        {
            Coordinates = new ReadOnlyCollection<IPosition>(
                coordinates?.ToArray() ?? throw new ArgumentNullException(nameof(coordinates)));

            if (Coordinates.Count < 2)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(coordinates),
                    "According to the GeoJSON v1.0 spec a LineString must have at least two or more positions.");
            }
        }

        public override GeoJSONObjectType Type => GeoJSONObjectType.LineString;

        [JsonProperty("coordinates", Required = Required.Always)]
        [JsonConverter(typeof(PositionEnumerableConverter))]
        public ReadOnlyCollection<IPosition> Coordinates { get; }

        public bool IsClosed()
        {
            var firstCoordinate = Coordinates[0];
            var lastCoordinate = Coordinates[Coordinates.Count - 1];

            return firstCoordinate.Longitude.Equals(lastCoordinate.Longitude)
                   && firstCoordinate.Latitude.Equals(lastCoordinate.Latitude)
                   && Nullable.Equals(firstCoordinate.Altitude, lastCoordinate.Altitude);
        }

        public bool IsLinearRing()
        {
            return Coordinates.Count >= 4 && IsClosed();
        }
    }
}
