using Application.GeoJson.Geometry;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.GeoJson.Converters
{
    public class LineStringEnumerableConverter : JsonConverter
    {
        private static readonly PositionEnumerableConverter LineStringConverter = new PositionEnumerableConverter();

        public override bool CanConvert(Type objectType)
        {
            return typeof(IEnumerable<LineString>).IsAssignableFromType(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var rings = existingValue as JArray ?? serializer.Deserialize<JArray>(reader);
            return rings.Select(ring => new LineString((IEnumerable<IPosition>)LineStringConverter.ReadJson(
                    reader,
                    typeof(IEnumerable<IPosition>),
                    ring,
                    serializer)))
                .ToArray();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value is IEnumerable<LineString> coordinateElements)
            {
                writer.WriteStartArray();
                foreach (var subPolygon in coordinateElements)
                {
                    LineStringConverter.WriteJson(writer, subPolygon.Coordinates, serializer);
                }
                writer.WriteEndArray();
            }
            else
            {
                throw new ArgumentException($"{nameof(LineStringEnumerableConverter)}: unsupported value type");
            }
        }
    }
}
