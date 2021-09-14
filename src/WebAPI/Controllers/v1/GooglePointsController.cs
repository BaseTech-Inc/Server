using Application.Common.GooglePoints;
using Application.GeoJson.Features;
using Application.GeoJson.Geometry;
using Domain.Entities;
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
        public GooglePointsController()
        { }

        // GET: api/v1/GooglePoints/encode-geojson/
        [HttpPost("encode-geojson")]
        public async Task<ActionResult<string>> EncodeGeojosn(
            [FromBody] Feature<LineString> points)
        {
            var googlePointsResult = GooglePoint.EncodeGeojson(points);

            if (googlePointsResult == null)
            {
                return BadRequest(googlePointsResult);
            }

            return Ok(googlePointsResult);
        }

        // GET: api/v1/GooglePoints/encode-geojson/
        [HttpPost("encode-coordinates")]
        public async Task<ActionResult<string>> EncodeCoordinates(
            [FromBody] IEnumerable<Ponto> points)
        {
            var googlePointsResult = GooglePoint.EncodeCoordinate(points);

            if (googlePointsResult == null)
            {
                return BadRequest(googlePointsResult);
            }

            return Ok(googlePointsResult);
        }

        // GET: api/v1/GooglePoints/decode-coordinates/
        [HttpGet("decode-coordinates")]
        public async Task<ActionResult<IEnumerable<Ponto>>> DecodeCoordinates(
            string encodedPoints)
        {
            var googlePointsResult = GooglePoint.DecodeCoordinates(encodedPoints);

            if (googlePointsResult == null)
            {
                return BadRequest(googlePointsResult);
            }

            return Ok(googlePointsResult);
        }

        // GET: api/v1/GooglePoints/decode-geojson/
        [HttpGet("decode-geojson")]
        public async Task<ActionResult<Feature<LineString>>> DecodeGeojson(
            string encodedPoints)
        {
            var googlePointsResult = GooglePoint.DecodeGeojson(encodedPoints);

            if (googlePointsResult == null)
            {
                return BadRequest(googlePointsResult);
            }

            return Ok(googlePointsResult);
        }
    }
}
