﻿using Application.Common.Models;
using Infrastructure.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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



            var htmlText = await HttpRequestUrl.ProcessHttpClient<IList<NominatimDto>>(url);

            return htmlText;
        }
    }
}
