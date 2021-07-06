using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.GeoJson.Features
{
    public class FeatureCollection : GeoJSONObject
    {
        public FeatureCollection() : this(new List<Feature>())
        {
        }

        public FeatureCollection(List<Feature> features)
        {
            if (features == null)
            {
                throw new ArgumentNullException(nameof(features));
            }

            Features = features;
        }

        public override GeoJSONObjectType Type => GeoJSONObjectType.FeatureCollection;

        [JsonProperty(PropertyName = "features", Required = Required.Always)]
        public List<Feature> Features { get; private set; }
    }
}
