using Application.GeoJson.Geometry;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.GeoJson.Converters
{
    public class PointEnumerableConverter : JsonConverter
    {
        private static readonly PositionConverter PositionConverter = new PositionConverter();

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value is IEnumerable<Point> points)
            {
                writer.WriteStartArray();
                foreach (var point in points)
                {
                    PositionConverter.WriteJson(writer, point.Coordinates, serializer);
                }
                writer.WriteEndArray();
            }
            else
            {
                throw new ArgumentException($"{nameof(PointEnumerableConverter)}: unsupported value {value}");
            }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var coordinates = existingValue as JArray ?? serializer.Deserialize<JArray>(reader);
            return coordinates.Select(position => new Point(position.ToObject<IEnumerable<double>>().ToPosition()));
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(IEnumerable<Point>);
        }
    }
}
