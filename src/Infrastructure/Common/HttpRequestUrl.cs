using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure.Common
{
    public static class HttpRequestUrl
    {
        public static string SetBaseAdress(this string url, string baseadress)
        {
            return baseadress;
        }

        public static string AddPath(this string url, string path)
        {
            return url + path;
        }

        public static string SetQueryParams(this string url, Object queryParams)
        {
            url += "?";

            Type typeQueryParams = queryParams.GetType();
            PropertyInfo[] propsQueryParams = typeQueryParams.GetProperties();

            List<String> param = new List<String>();

            foreach (PropertyInfo propQueryParam in propsQueryParams)
            {
                if (propQueryParam.CanRead)
                {
                    param.Add(propQueryParam.GetValue(queryParams).ToString());

                    url += $"{ propQueryParam.Name }={ propQueryParam.GetValue(queryParams) }&";
                }
            }

            return url;
        }

        private static readonly HttpClient client = new HttpClient();

        public static async Task<T> ProcessHttpClient<T>(
            string url, 
            JsonSerializerOptions options = null, 
            string mediaType = "application/json")
        {
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue(mediaType));
            client.DefaultRequestHeaders.Add("Referer", "http://localhost:5001");

            var streamTask = client.GetStreamAsync(url);
            var objectResult = await JsonSerializer.DeserializeAsync<T>(await streamTask);

            return objectResult;
        }

        public static async Task<String> ProcessHttpClient(
            string url, 
            string mediaType = "application/json")
        {
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue(mediaType));

            var streamTask = await client.GetStringAsync(url);

            return streamTask;
        }
    }
}
