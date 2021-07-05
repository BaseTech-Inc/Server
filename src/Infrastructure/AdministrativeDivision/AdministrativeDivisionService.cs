using Infrastructure.Common;
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

        public async Task<List<T>> ProcessDivisionsLocations<T>(string path)
        {
            string url = baseUrlLocations
                .AddPath(path);

            var division = await HttpRequestUrl.ProcessHttpClient<List<T>>(url);

            return division;
        }

        public async Task<T> ProcessDivisionsMeshes<T>(string path, string identifier)
        {
            HttpClient client = new HttpClient();

            string url = baseUrlMeshes
                .AddPath(path)
                .AddPath(identifier)
                .SetQueryParams(new
                {
                    qualidade = "intermediaria",
                    formato = "application/vnd.geo+json"
                });

            var division = await HttpRequestUrl.ProcessHttpClient<T>(url);
            

            return division;
        }
    }
}
