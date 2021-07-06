﻿using Infrastructure.GeoJson.Converters;
using Infrastructure.GeoJson.Geometry;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.GeoJson.Features
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
