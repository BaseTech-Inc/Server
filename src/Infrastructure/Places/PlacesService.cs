using Application.Common.Interfaces;
using Infrastructure.Common;
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
    public class PlacesService : IPlacesService
    {
        public PlacesService()
        { }

        private static readonly string baseUrlLocations = "https://servicodados.ibge.gov.br/api/v1/localidades";

        public async Task<List<T>> ProcessPlaces<T>(string path)
        {
            string url = baseUrlLocations
                .AddPath(path);

            var division = await HttpRequestUrl.ProcessHttpClient<List<T>>(url);

            return division;
        }        
    }
}
