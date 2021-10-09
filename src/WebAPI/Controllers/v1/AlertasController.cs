using Application.Alertas.Commands.CreateAlertas;
using Application.Alertas.Commands.DeleteAlertas;
using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace WebAPI.Controllers.v1
{
    [Authorize]
    public class AlertasController : ApiControllerBase
    {
        private readonly ICGESPService _CGESPService;

        public AlertasController(
            ICGESPService CGESPService)
        {
            _CGESPService = CGESPService;
        }

        // GET: api/v1/Alertas?year=year&month=month&day=day
        [HttpGet]
        public async Task<ActionResult<Response<IList<Alerta>>>> Get(
            int year,
            int month,
            int day)
        {
            var response = await _CGESPService.ProcessCGESPByDate(new DateTime(year, month, day));

            if (!response.Succeeded)
            {
                return NotFound(response);
            }

            return Ok(
                response
                );
        }

        // GET: api/v1/Alertas/Bairro?year=year&month=month&day=day&district=district
        [HttpGet("Bairro")]
        public async Task<ActionResult<Response<IList<Alerta>>>> GetByDistrict(
            int year,
            int month,
            int day,
            string district)
        {
            var response = await _CGESPService.ProcessCGESPByDistrict(new DateTime(year, month, day), district);

            if (!response.Succeeded)
            {
                return NotFound(response);
            }

            return Ok(
                response
                );
        }

        // GET: api/v1/Alertas/Pagination?year=year&month=month&day=day&PageNumber=PageNumber
        [HttpGet("Pagination")]
        public async Task<ActionResult<Response<PaginatedList<Alerta>>>> GetWithPagination(
            int year,
            int month,
            int day,
            int PageNumber = 1,
            int PageSize = 10)
        {
            var response = await _CGESPService.ProcessCGESPWithPagination(new DateTime(year, month, day), PageNumber, PageSize);

            if (!response.Succeeded)
            {
                return NotFound(response);
            }

            return Ok(
                response
                );
        }

        // POST: api/v1/Alertas?Latitude=Latitude&Longitude=Longitude&Distrito=Distrito&TempoInicio=TempoInicio&...
        [HttpPost]
        [Authorize(Policy = "ElevatedRights")]
        public async Task<ActionResult<Response<string>>> Create(
            [FromServices] ICreateAlertasCommandHandler handler,
            [FromQuery] CreateAlertasCommand command)
        {
            var response = await handler.Handle(command);

            if (!response.Succeeded)
            {
                return BadRequest(response);
            }

            return Created(
                HttpRequestHeader.Referer.ToString(),
                response
                );
        }

        // DELETE: api/v1/Alertas/?Id=Id
        /// <summary>
        /// Não é para passar o userId
        /// </summary>
        [HttpDelete]
        [Authorize(Policy = "ElevatedRights")]
        public async Task<ActionResult<Response<string>>> Delete(
            [FromServices] IDeleteAlertasCommandHandler handler,
            [FromQuery] DeleteAlertasCommand command
        )
        {
            var response = await handler.Handle(command);

            if (!response.Succeeded)
            {
                return NotFound(response);
            }

            return Ok(
                response
                );
        }
    }
}
