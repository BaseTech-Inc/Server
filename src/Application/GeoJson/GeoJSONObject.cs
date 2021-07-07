using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Application.GeoJson
{
    public abstract class GeoJSONObject : IGeoJSONObject
    {
        [JsonProperty(PropertyName = "type", Required = Required.Always, DefaultValueHandling = DefaultValueHandling.Include)]
        [JsonConverter(typeof(StringEnumConverter))]
        public abstract GeoJSONObjectType Type { get; }
    }
}
