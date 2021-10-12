using Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IOpenWeatherService
    {
        Task<Response<CurrentWeatherResponse>> ProcessCurrentByCoord(
            double lat,
            double lon);

        Task<Response<CurrentWeatherResponse>> ProcessCurrentByName(
            string street,
            string district,
            string city = "São Paulo",
            string state = "São Paulo");

        Task<Response<ForecastResponse>> ProcessForecastByCoord(
            double lat,
            double lon);

        Task<Response<ForecastResponse>> ProcessForecastByName(
            string street,
            string district,
            string city = "São Paulo",
            string state = "São Paulo");
    }
}
