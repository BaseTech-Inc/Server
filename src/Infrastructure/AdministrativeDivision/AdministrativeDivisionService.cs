using Infrastructure.Common;
using Infrastructure.GeoJson.Features;
using Infrastructure.GeoJson.Geometry;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure.AdministrativeDivision
{
    public class AdministrativeDivisionService
    {
        const string baseUrlLocations = "https://servicodados.ibge.gov.br/api/v1/localidades";
        const string baseUrlMeshes = "https://servicodados.ibge.gov.br/api/v3/malhas";

        public static async Task<List<T>> ProcessDivisionsLocations<T>(string path)
        {
            string url = baseUrlLocations
                .AddPath(path);

            var division = await HttpRequestUrl.ProcessHttpClient<List<T>>(url);

            return division;
        }

        public static async Task<Feature<TGeometry>> ProcessDivisionsMeshes<TGeometry>(string path, string identifier)
            where TGeometry : IGeometryObject
        {
            HttpClient client = new HttpClient();

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

            var feature = JsonConvert.DeserializeObject<Feature<TGeometry>>(JsonConvert.SerializeObject(featureCollection.Features[0]));

            return feature;
        }

        public static async Task<Feature<IGeometryObject>> ProcessDivisionsMeshes(string path, string identifier)
        {
            var feature = await ProcessDivisionsMeshes<IGeometryObject>(path, identifier);

            return feature;
        }
    }
}
