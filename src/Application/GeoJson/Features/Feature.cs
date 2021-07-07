using Application.GeoJson.Converters;
using Application.GeoJson.Geometry;
using Newtonsoft.Json;

namespace Application.GeoJson.Features
{
    public class Feature<TGeometry> : GeoJSONObject
       where TGeometry : IGeometryObject
    {
        [JsonConstructor]
        public Feature(TGeometry geometry, string id = null)
        {
            Geometry = geometry;
            Id = id;
        }

        public override GeoJSONObjectType Type => GeoJSONObjectType.Feature;

        [JsonProperty(PropertyName = "id", NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; }

        [JsonProperty(PropertyName = "geometry", Required = Required.AllowNull)]
        [JsonConverter(typeof(GeometryConverter))]
        public TGeometry Geometry { get; }
    }

    public class Feature : Feature<IGeometryObject>
    {
        [JsonConstructor]
        public Feature(IGeometryObject geometry, string id = null)
            : base(geometry, id)
        {
        }
    }
}
