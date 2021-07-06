using Infrastructure.GeoJson.Geometry;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.GeoJson.Converters
{
    public class GeometryConverter : JsonConverter
    {
        public override bool CanWrite => false;

        public override bool CanConvert(Type objectType)
        {
            return typeof(IGeometryObject).IsAssignableFromType(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            switch (reader.TokenType)
            {
                case JsonToken.Null:
                    return null;
                case JsonToken.StartObject:
                    var value = JObject.Load(reader);
                    return ReadGeoJson(value);
                case JsonToken.StartArray:
                    var values = JArray.Load(reader);
                    var geometries = new ReadOnlyCollection<IGeometryObject>(
                        values.Cast<JObject>().Select(ReadGeoJson).ToArray());
                    return geometries;
            }

            throw new JsonReaderException("expected null, object or array token but received " + reader.TokenType);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException("Unnecessary because CanWrite is false. The type will skip the converter.");
        }

        private static IGeometryObject ReadGeoJson(JObject value)
        {
            JToken token;

            if (!value.TryGetValue("type", StringComparison.OrdinalIgnoreCase, out token))
            {
                throw new JsonReaderException("json must contain a \"type\" property");
            }

            GeoJSONObjectType geoJsonType;

            if (!Enum.TryParse(token.Value<string>(), true, out geoJsonType))
            {
                throw new JsonReaderException("type must be a valid geojson geometry object type");
            }

            switch (geoJsonType)
            {
                case GeoJSONObjectType.Point:
                    return value.ToObject<Point>();
                case GeoJSONObjectType.LineString:
                    return value.ToObject<LineString>();
                case GeoJSONObjectType.Polygon:
                    return value.ToObject<Polygon>();
                case GeoJSONObjectType.MultiPolygon:
                    return value.ToObject<MultiPolygon>();
                case GeoJSONObjectType.Feature:
                case GeoJSONObjectType.FeatureCollection:
                default:
                    throw new NotSupportedException("Feature and FeatureCollection types are Feature objects and not Geometry objects");
            }
        }
    }
}
