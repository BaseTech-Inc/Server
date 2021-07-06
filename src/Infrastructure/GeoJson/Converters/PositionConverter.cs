using Infrastructure.GeoJson.Geometry;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.GeoJson.Converters
{
    public class PositionConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(IPosition).IsAssignableFromType(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            double[] coordinates;

            try
            {
                coordinates = serializer.Deserialize<double[]>(reader);
            }
            catch (Exception e)
            {
                throw new JsonReaderException("error parsing coordinates", e);
            }
            return coordinates?.ToPosition() ?? throw new JsonReaderException("coordinates cannot be null");
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value is IPosition coordinates)
            {
                writer.WriteStartArray();

                writer.WriteValue(coordinates.Longitude);
                writer.WriteValue(coordinates.Latitude);

                if (coordinates.Altitude.HasValue)
                {
                    writer.WriteValue(coordinates.Altitude.Value);
                }

                writer.WriteEndArray();
            }
            else
            {
                throw new NotImplementedException();
            }
        }
    }
}
