using Application.GeoJson.Geometry;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.GeoJson.Converters
{
    public class PositionEnumerableConverter : JsonConverter
    {
        private static readonly PositionConverter PositionConverter = new PositionConverter();

        public override bool CanConvert(Type objectType)
        {
            return typeof(IEnumerable<IPosition>).IsAssignableFromType(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var coordinates = existingValue as JArray ?? serializer.Deserialize<JArray>(reader);
            return coordinates.Select(pos => PositionConverter.ReadJson(pos.CreateReader(),
                typeof(IPosition),
                pos,
                serializer
            )).Cast<IPosition>();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value is IEnumerable<IPosition> coordinateElements)
            {
                writer.WriteStartArray();
                foreach (var position in coordinateElements)
                {
                    PositionConverter.WriteJson(writer, position, serializer);
                }
                writer.WriteEndArray();
            }
            else
            {
                throw new ArgumentException($"{nameof(PositionEnumerableConverter)}: unsupported value type");
            }
        }
    }
}
