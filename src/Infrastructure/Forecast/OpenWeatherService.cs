using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using Infrastructure.Common;
using Infrastructure.Flooding;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure.Forecast
{
    public class OpenWeatherService : IOpenWeatherService
    {
        private readonly IConfiguration _configuration;

        public OpenWeatherService(
            IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private static readonly string baseUrl = "https://api.openweathermap.org/data/2.5/";

        public async Task<Response<OpenWeatherDto>> ProcessCurrentByCoord(
            double lat,
            double lon)
        {
            try
            {
                string url = baseUrl
                    .AddPath("weather")
                    .SetQueryParams(new
                    {
                        lat = lat,
                        lon = lon,
                        appid = _configuration["OpenWeather:appid"],
                        lang = "pt_br",
                        units = "metric"
                    });

                var streamTask = await HttpRequestUrl.ProcessHttpClient(url);

                var openWeatherDto = JsonSerializer.Deserialize<OpenWeatherDto>(streamTask);

                if (openWeatherDto != null)
                {
                    return new Response<OpenWeatherDto>(openWeatherDto, message: $"Success.");
                } else
                {
                    return new Response<OpenWeatherDto>(message: $"Could not find any place with that name.");
                }                
            } catch (Exception)
            {
                return new Response<OpenWeatherDto>(message: $"An error occurred please try again later.");
            }
        }

        public async Task<Response<OpenWeatherDto>> ProcessCurrentByName(
            string street,
            string district,
            string city = "São Paulo",
            string state = "São Paulo")
        {
            var nominatimService = new NominatimService();

            var nominatimResult = await nominatimService.ProcessNominatim(street, district, city, state);

            if (nominatimResult.Count > 0)
            {
                var lat = double.Parse(
                    nominatimResult[0].lat,
                    CultureInfo.InvariantCulture);
                var lon = double.Parse(
                    nominatimResult[0].lon,
                    CultureInfo.InvariantCulture);

                var ponto = new Ponto()
                {
                    Latitude = lat,
                    Longitude = lon
                };

                return await ProcessCurrentByCoord(ponto.Latitude, ponto.Longitude);
            }

            return new Response<OpenWeatherDto>(message: $"An error occurred please try again later.");
        }
    }
}
