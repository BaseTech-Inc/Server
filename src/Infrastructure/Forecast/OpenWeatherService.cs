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

        private static async Task<Ponto> GetLatitudeLongitude(
            string street,
            string district,
            string city,
            string state)
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

                return ponto;
            }

            return null;
        }

        public async Task<Response<CurrentWeatherResponse>> ProcessCurrentByCoord(
            double lat,
            double lon)
        {
            try
            {
                string url = baseUrl
                    .AddPath("weather")
                    .SetQueryParams(new
                    {
                        lat,
                        lon,
                        appid = _configuration["OpenWeather:appid"],
                        lang = "pt_br",
                        units = "metric"
                    });

                var streamTask = await HttpRequestUrl.ProcessHttpClient(url);

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var openWeatherDto = JsonSerializer.Deserialize<CurrentWeatherDto>(streamTask, options);

                if (openWeatherDto != null)
                {
                    var nominatimService = new NominatimService();

                    var nominatimResult = await nominatimService.ProcessNominatimByCoords(lat.ToString(), lon.ToString());

                    var forecastResponse = new CurrentWeatherResponse()
                    {
                        q = $"{ nominatimResult.address.suburb }, { nominatimResult.address.city }",
                        Coord = new CoordCurrent
                        {
                            Lat = openWeatherDto.Coord.Lat,
                            Lon = openWeatherDto.Coord.Lon
                        },
                        Main = new MainCurrentWeather
                        {
                            Feels_like = openWeatherDto.Main.Feels_like,
                            Humidity = openWeatherDto.Main.Humidity,
                            Temp = openWeatherDto.Main.Temp,
                            Temp_max = openWeatherDto.Main.Temp_max,
                            Temp_min = openWeatherDto.Main.Temp_min,
                        },
                        Weather = new WeatherCurrentWeather
                        {
                            Description = openWeatherDto.Weather.FirstOrDefault().Description,
                            Icon = openWeatherDto.Weather.FirstOrDefault().Icon,
                            Main = openWeatherDto.Weather.FirstOrDefault().Main
                        }                        
                    };

                    return new Response<CurrentWeatherResponse>(forecastResponse, message: $"Success.");
                } else
                {
                    return new Response<CurrentWeatherResponse>(message: $"Could not find any place with that name.");
                }                
            } catch (Exception)
            {
                return new Response<CurrentWeatherResponse>(message: $"An error occurred please try again later.");
            }
        }

        public async Task<Response<CurrentWeatherResponse>> ProcessCurrentByName(
            string street,
            string district,
            string city = "São Paulo",
            string state = "São Paulo")
        {
            var ponto = await GetLatitudeLongitude(street, district, city, state);

            if (ponto != null) 
            { 
                return await ProcessCurrentByCoord(ponto.Latitude, ponto.Longitude);
            }

            return new Response<CurrentWeatherResponse>(message: $"An error occurred please try again later.");
        }

        public async Task<Response<ForecastResponse>> ProcessForecastByCoord(
            double lat,
            double lon)
        {
            try
            {
                string url = baseUrl
                    .AddPath("onecall")
                    .SetQueryParams(new
                    {
                        lat,
                        lon,
                        exclude = "current,minutely,alerts",
                        appid = _configuration["OpenWeather:appid"],
                        lang = "pt_br",
                        units = "metric"
                    });

                var streamTask = await HttpRequestUrl.ProcessHttpClient(url);

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var forecastDto = JsonSerializer.Deserialize<ForecastDto>(streamTask, options);

                if (forecastDto != null)
                {
                    var nominatimService = new NominatimService();

                    var nominatimResult = await nominatimService.ProcessNominatimByCoords(lat.ToString(), lon.ToString());

                    var forecastResponse = new ForecastResponse()
                    {
                        q = $"{ nominatimResult.address.suburb }, { nominatimResult.address.city }",
                        Coord = new CoordForecast
                        {
                            Lat = forecastDto.Lat,
                            Lon = forecastDto.Lon
                        },
                        Hourly = forecastDto.Hourly,
                        Daily = forecastDto.Daily
                    };

                    return new Response<ForecastResponse>(forecastResponse, message: $"Success.");
                }
                else
                {
                    return new Response<ForecastResponse>(message: $"Could not find any place with that name.");
                }
            }
            catch (Exception)
            {
                return new Response<ForecastResponse>(message: $"An error occurred please try again later.");
            }
        }

        public async Task<Response<ForecastResponse>> ProcessForecastByName(
            string street,
            string district,
            string city = "São Paulo",
            string state = "São Paulo")
        {
            var ponto = await GetLatitudeLongitude(street, district, city, state);

            if (ponto != null)
            {
                return await ProcessForecastByCoord(ponto.Latitude, ponto.Longitude);
            }

            return new Response<ForecastResponse>(message: $"An error occurred please try again later.");
        }
    }
}
