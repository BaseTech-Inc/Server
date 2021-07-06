using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.GeoJson
{
    public abstract class GeoJSONObject : IGeoJSONObject
    {
        [JsonProperty(PropertyName = "type", Required = Required.Always, DefaultValueHandling = DefaultValueHandling.Include)]
        [JsonConverter(typeof(StringEnumConverter))]
        public abstract GeoJSONObjectType Type { get; }
    }
}
