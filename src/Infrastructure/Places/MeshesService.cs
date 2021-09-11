using Newtonsoft.Json;
using System.Threading.Tasks;

using Application.Common.Interfaces;
using Application.GeoJson.Features;
using Application.GeoJson.Geometry;
using Infrastructure.Common;

namespace Infrastructure.Places
{
    public class MeshesService : IMeshesService
    {
        private readonly string baseUrlMeshes = "https://servicodados.ibge.gov.br/api/v3/malhas";

        public async Task<Feature<TGeometry>> ProcessMeshes<TGeometry>(string path, string identifier)
            where TGeometry : IGeometryObject
        {
            string url = baseUrlMeshes
                .AddPath(path)
                .AddPath(identifier)
                .SetQueryParams(new
                {
                    qualidade = "minima",
                    formato = "application/vnd.geo+json"
                });

            var json = await HttpRequestUrl.ProcessHttpClient(url);

            var featureCollection = JsonConvert.DeserializeObject<FeatureCollection>(json);

            var feature = JsonConvert.DeserializeObject<Feature<TGeometry>>(
                JsonConvert.SerializeObject(featureCollection.Features[0]));

            return feature;
        }

        public async Task<Feature<IGeometryObject>> ProcessMeshes(string path, string identifier)
        {
            var feature = await ProcessMeshes<IGeometryObject>(path, identifier);

            return feature;
        }
    }
}
