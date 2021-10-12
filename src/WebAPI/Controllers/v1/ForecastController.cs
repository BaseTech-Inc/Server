using Application.Common.Interfaces;
using Application.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers.v1
{
    [Authorize]
    public class ForecastController : ApiControllerBase
    {
        private readonly IOpenWeatherService _openWeatherService;

        public ForecastController(
            IOpenWeatherService openWeatherService)
        {
            _openWeatherService = openWeatherService;
        }

        // GET: api/v1/Forecast/coord
        [HttpGet("coord")]
        public async Task<ActionResult<Response<ForecastResponse>>> GetByCoord(
            float lat,
            float lon)
        {
            var response = await _openWeatherService.ProcessForecastByCoord(lat, lon);

            if (!response.Succeeded)
            {
                return BadRequest(response);
            }

            return Ok(
                response
                );
        }
    }
}
