using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Common.Models;
using Infrastructure.Common;

namespace Infrastructure.Flooding
{
    public class NominatimService
    {
        public NominatimService()
        { }

        private static readonly string baseUrl = "Https://nominatim.openstreetmap.org/";

        public async Task<IList<NominatimDto>> ProcessNominatim(
            string street,
            string district,
            string city,
            string state)
        {
            string url;

            if (street != null)
            {
                if (street.StartsWith("Av.") || street.StartsWith("Tn."))
                {
                    url = baseUrl
                        .SetQueryParams(new
                        {
                            q = $"{ street.StreetConversion() }, { city }, { state }",
                            polygon_geojson = 1,
                            format = "jsonv2"
                        });
                }
                else
                {
                    url = baseUrl
                        .SetQueryParams(new
                        {
                            q = $"{ street.StreetConversion() }, { district }, { city }, { state }",
                            polygon_geojson = 1,
                            format = "jsonv2"
                        });
                }
            } else
            {
                url = baseUrl
                    .SetQueryParams(new
                    {
                        q = $"{ district }, { city }, { state }",
                        polygon_geojson = 1,
                        format = "jsonv2"
                    });
            }

            var htmlText = await HttpRequestUrl.ProcessHttpClient<IList<NominatimDto>>(url);

            return htmlText;
        }

        public async Task<NominatimDto> ProcessNominatimByCoords(
            string lat,
            string lon)
        {
            string url;

            url = baseUrl
                .AddPath("reverse")
                .SetQueryParams(new
                {
                    lat = lat.Replace(",", "."),
                    lon = lon.Replace(",", "."),
                    format = "jsonv2"
                });


            var htmlText = await HttpRequestUrl.ProcessHttpClient<NominatimDto>(url);

            return htmlText;
        }
    }
}
