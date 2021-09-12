using Application.Common.Interfaces;
using Application.Common.Models;
using Application.GeoJson.Features;
using Application.GeoJson.Geometry;
using Infrastructure.GooglePoint;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers.v1
{
    [Authorize]
    public class GooglePointsController : ApiControllerBase
    {
        private readonly IGooglePointService _googlePointService;

        public GooglePointsController(
            IGooglePointService googlePointService)
        {
            _googlePointService = googlePointService;
        }

        // GET: api/v1/GooglePoints/encode-geojson/
        [HttpPost("encode-geojson")]
        public async Task<ActionResult<string>> EncodeGeojosn(
            [FromBody] Feature<LineString> points)
        {
            var googlePointsResult = _googlePointService.EncodeGeojson(points);

            if (googlePointsResult == null)
            {
                return BadRequest(googlePointsResult);
            }

            return Ok(googlePointsResult);
        }

        // GET: api/v1/GooglePoints/encode-geojson/
        [HttpPost("encode-coordinates")]
        public async Task<ActionResult<string>> EncodeCoordinates(
            [FromBody] IEnumerable<CoordinateEntity> points)
        {
            var googlePointsResult = _googlePointService.EncodeCoordinate(points);

            if (googlePointsResult == null)
            {
                return BadRequest(googlePointsResult);
            }

            return Ok(googlePointsResult);
        }

        // GET: api/v1/GooglePoints/decode/
        [HttpGet("decode-coordinates")]
        public async Task<ActionResult<IEnumerable<CoordinateEntity>>> DecodeCoordinates(
            string encodedPoints)
        {
            var googlePointsResult = _googlePointService.DecodeCoordinates(encodedPoints);

            if (googlePointsResult == null)
            {
                return BadRequest(googlePointsResult);
            }

            return Ok(googlePointsResult);
        }
    }
}
