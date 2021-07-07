using Application.GeoJson.Geometry;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.GeoJson.Converters
{
    public class PolygonEnumerableConverter : JsonConverter
    {

        private static readonly LineStringEnumerableConverter PolygonConverter = new LineStringEnumerableConverter();

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(IEnumerable<Polygon>);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var o = serializer.Deserialize<JArray>(reader);
            var polygons =
                o.Select(
                    polygonObject => (IEnumerable<LineString>)PolygonConverter.ReadJson(
                            polygonObject.CreateReader(),
                            typeof(IEnumerable<LineString>),
                            polygonObject, serializer))
                    .Select(lines => new Polygon(lines))
                    .ToList();

            return polygons;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value is IEnumerable<Polygon> polygons)
            {
                writer.WriteStartArray();
                foreach (var polygon in polygons)
                {
                    PolygonConverter.WriteJson(writer, polygon.Coordinates, serializer);
                }
                writer.WriteEndArray();
            }
            else
            {
                throw new ArgumentException($"{nameof(PointEnumerableConverter)}: unsupported value {value}");
            }
        }
    }
}
