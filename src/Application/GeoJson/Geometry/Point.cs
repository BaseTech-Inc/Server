using Newtonsoft.Json;
using System;

namespace Application.GeoJson.Geometry
{
    public class Point : GeoJSONObject, IGeometryObject
    {
        public Point(IPosition coordinates)
        {
            Coordinates = coordinates ?? throw new ArgumentNullException(nameof(coordinates));
        }

        public override GeoJSONObjectType Type => GeoJSONObjectType.Point;

        [JsonProperty("coordinates", Required = Required.Always)]
        [JsonConverter(typeof(PositionConverter))]
        public IPosition Coordinates { get; }
    }

    internal class PositionConverter
    {
    }
}
